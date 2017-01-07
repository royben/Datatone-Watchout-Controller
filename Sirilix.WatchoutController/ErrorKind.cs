using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    /// <summary>
    /// Represents the enumeration for the different kinds of the Watchout server errors.
    /// </summary>
    public enum ErrorKind
    {
        /// <summary>
        /// Indicates a generic operating system error from the host’s OS. Under
        /// Windows, this is a HRESULT that indicates failure, with the error code included
        /// as the second parameter (possibly decoded into an error message string). The
        /// third parameter may provide additional information.
        /// </summary>
        [Description("Operating system error (for instance, a Win32 HRESULT).")]
        OPERATING_SYSTEM_ERROR = 1,

        /// <summary>
        /// Similar to the Operating System Error, but originating from QuickTime. This is
        /// treated separately from the OS errors since the QT errors use MacOS style
        /// error codes even under Windows. This kind of error typically originates from
        /// still image files, or from video files as they are opened or played. The third
        /// parameter generally contains the name of the offending media file.
        /// </summary>
        [Description("QuickTime error (Mac OSErr style).")]
        QUICKTIME_ERROR = 2,

        /// <summary>
        /// Error occurred specifically related to rendering. This is similar to other operating
        /// system errors, except that you also know that it occurred while rendering.
        /// Sometimes, rendering errors occur due to display card driver issues, video
        /// memory or other hardware resource limitations.
        /// </summary>
        [Description("Rendering API error (that is, DirectX).")]
        RENDERING_API_ERROR = 3,

        /// <summary>
        /// Error occurred specifically related to network communication. This is similar to
        /// other operating system errors, except that you also know that it occurred
        /// specifically while using the network. Sometimes, network errors are caused by
        /// network interface hardware or driver issues, the computer’s network configuration,
        /// or problems on the network itself (for instance, a bad cable/hub or
        /// incorrectly configured router).
        /// </summary>
        [Description("Network errors (that is, WinSock).")]
        NETWORK_ERRORS = 4,

        /// <summary>
        /// Error occurred when attempting to get a file from the media file server. The
        /// error number the same as those listed for the first Reply parameter in the File
        /// Transfer group. The Excuse string is typically the name of the required file.
        /// </summary>
        [Description("File server error (for example, file not found during download).")]
        FILE_SERVER_ERROR = 5,

        /// <summary>
        /// Indicates an error that occurred when reading structured data (such as a show
        /// specification file). Error code and excuse vary with the nature of the error. 
        /// </summary>
        [Description("Syntax/parser error (for instance, when loading a specification file).")]
        SYNTAX_PARSER_ERROR = 6,

        /// <summary>
        /// Other errors, not covered by any of the above cases. Always described further
        /// by a string as the second parameter, as well as further information in the third
        /// parameter (optional).
        /// </summary>
        [Description("General runtime error.")]
        GENERAL_RUNTIME_ERROR = 7,

        /// <summary>
        /// Authentication error.
        /// </summary>
        [Description("Authentication error.")]
        AUTHENTICATION_ERROR = 8,
    }
}
