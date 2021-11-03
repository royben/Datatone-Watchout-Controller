using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    /// <summary>
    /// Represents a Watchout command formatter.
    /// </summary>
    public static class CommandFormatter
    {
        /// <summary>
        /// Serializes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>A string representation of the command.</returns>
        public static String Serialize(WatchoutCommand command)
        {
            String str = String.Format("[{0}]{1}", command.ID, command.Name);

            foreach (var prop in command.GetType().GetProperties().Where(pi => !pi.GetCustomAttributes(typeof(WatchoutIgnoreAttribute), true).Any()))
            {
                var o = prop.GetValue(command);

                if (o != null)
                {
                    String value = String.Empty;

                    if (prop.PropertyType == typeof(String))
                    {
                        value = "\"" + o.ToString() + "\"";
                    }
                    else if (prop.PropertyType == typeof(bool))
                    {
                        value = o.ToString().ToLower();
                    }
                    else if (prop.PropertyType == typeof(bool?))
                    {
                        bool? b = o as bool?;
                        value = b != null ? b.ToString().ToLower() : "false";
                    }
                    else if (prop.PropertyType == typeof(TimeSpan))
                    {
                        value = "\"" + ((TimeSpan)o).ToString(@"hh\:mm\:ss\.fff") + "\"";
                    }
                    else
                    {
                        value = o.ToString();
                    }

                    str += " " + value;
                }
            }

            return str + "\r\n";
        }

        /// <summary>
        /// Deserializes the specified response string and returns the proper <see cref="WatchoutFeedback"/>.
        /// </summary>
        /// <param name="response">The response string.</param>
        /// <returns>Returns the proper <see cref="WatchoutFeedback"/>.</returns>
        public static WatchoutFeedback Deserialize(String response)
        {
            response = RemoveCarriageReturn(response);

            var feedbackTypes = Assembly.GetAssembly(typeof(CommandFormatter)).GetTypes().Where(t => t != typeof(WatchoutFeedback) && typeof(WatchoutFeedback).IsAssignableFrom(t));

            List<String> parameters = Regex.Matches(response, @"[\""].+?[\""]|[^ ]+")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();

            String commandName = parameters.FirstOrDefault();

            if (parameters.Count == 1 && commandName.Contains("["))
            {
                return new WatchoutFeedback() { ID = commandName.Replace("[", "").Replace("]", "") };
            }

            parameters.RemoveAt(0);

            String commandId = null;

            if (commandName.Contains("["))
            {
                commandName = commandName.Remove(0, 1);

                String[] commandAndID = commandName.Split(']');
                commandId = commandAndID[0];
                commandName = commandAndID[1];
            }

            foreach (var type in feedbackTypes)
            {
                String fName = type.GetAttributeValue((FeedbackNameAttribute att) => att.Name);

                if (fName == commandName)
                {
                    WatchoutFeedback feedback = Activator.CreateInstance(type) as WatchoutFeedback;
                    feedback.ID = commandId;

                    var props = type.GetProperties().Where(pi => !pi.GetCustomAttributes(typeof(WatchoutIgnoreAttribute), true).Any()).ToList();

                    for (int i = 0; i < parameters.Count; i++)
                    {
                        if (i > props.Count - 1) break;

                        var prop = props[i];

                        if (prop.PropertyType == typeof(String))
                        {
                            prop.SetValue(feedback, Convert.ChangeType(parameters[i].Replace("\"", ""), prop.PropertyType));
                        }
                        else if (prop.PropertyType == typeof(TimeSpan))
                        {
                            prop.SetValue(feedback, TimeSpan.FromMilliseconds(Convert.ToInt32(parameters[i])));
                        }
                        else if (prop.PropertyType.IsEnum)
                        {
                            int number = -1;
                            if (int.TryParse(parameters[i], out number))
                            {
                                prop.SetValue(feedback, Convert.ChangeType(Enum.ToObject(prop.PropertyType, number), prop.PropertyType));
                            }
                        }
                        else
                        {
                            prop.SetValue(feedback, Convert.ChangeType(parameters[i], prop.PropertyType));
                        }
                    }

                    return feedback;
                }
            }

            Debug.WriteLine("Unrecognized feedback received. " + response);
            return null;
            //throw new UnrecognizedFeedbackException("Unrecognized feedback received. " + response);
        }

        /// <summary>
        /// Removes the carriage return occurrences in the specified string.
        /// </summary>
        /// <param name="response">The response string.</param>
        /// <returns>The processed string.</returns>
        private static String RemoveCarriageReturn(String response)
        {
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();
            return response.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty);
        }
    }
}
