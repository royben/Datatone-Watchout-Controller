# Datatone Watchout Controller
A Datatone Watchout TCP Client Libary.

This API aimes to provide a simple asynchronous communication over TCP/IP in order to control the Datatone Watchout Production/Display Software.

All API implementations are bound to the Datatone Watchout manual: https://www.dataton.com/assets/Products/Watchout/WATCHOUT_5_Users_Guide.pdf

<br/>
<hr/>
<br/>

The API includes two separate interfaces:
<br/>
<b>IWatchoutProductionClient</b> - for controlling the Watchout Production computer.
<br/>
<b>IWatchoutDisplayClient</b> - for controlling the Watchout Display computer.

The following commands are supported:

<h4>Production Computer</h4>
<ul>
<li><b>run</b> - Run timeline from current position, optional aux timeline name.</li>
<li><b>halt</b> - Stop at the current position, with optional auxiliary timeline name.</li>
<li><b>kill</b> - Stop and deactivate the named auxiliary timeline.</li>
<li><b>gotoTime</b> - Go to a time position, specified in milliseconds or as a time. The second, optional, parameter selects an auxiliary timeline.</li>
<li><b>gotoControlCue</b> - Go to a named Control cue (name is case sensitive).</li>
<li><b>standBy</b> - Set the standby mode to true or false.</li>
<li><b>load</b> - Load a show from specified file, with optional parameters.</li>
<li><b>online</b> - Control the online status of the production software.</li>
<li><b>update</b> - Update the display computers.</li>
<li><b>enableLayerCond</b> - Set enabled layer conditions.</li>
<li><b>setInput</b> - Set the value of a named Input, with optional fade-rate in mS.</li>
</ul>

<h4>Display Computer</h4>
<ul>
<li><b>ping</b> - Do-nothing command causing a Ready feedback message to be sent.</li>
<li><b>authenticate</b> - Perform authentication. Required prior to other commands.</li>
<li><b>load</b> - Load a show and get ready to run.</li>
<li><b>run</b> - Start running, optionally specifying an auxiliary timeline name.</li>
<li><b>halt</b> - Stop running, optionally specifying an auxiliary timeline name.</li>
<li><b>kill</b> - Stop and deactivate the named auxiliary timeline.</li>
<li><b>gotoTime</b> - Jump to a time position.</li>
<li><b>gotoControlCue</b> - Jump to the time position of a named Control cue.</li>
<li><b>enableLayerCond</b> - Turn conditional layers on or off.</li>
<li><b>standBy</b> - Enter/exit standby mode.</li>
<li><b>getStatus</b> - Retrieves name and status of the currently running show.</li>
<li><b>reset</b> - Reset and stop all timelines.</li>
<li><b>setInput</b> - Set the value of a named Input, with optional fade-rate in mS.</li>
<li><b>wait</b> - Waits for the entire display cluster to become established.</li>
</ul>

<h3>Usage Example<h3>

<pre><code class='language-cs'>

private async void Example()
{
   //Initialize a new production client.
   WatchoutProductionClient controller = new WatchoutProductionClient();
 
   //Connect the client to the production computer.
   controller.Connect("127.0.0.1");

   //Execute an auxiliary timline and wait for a response.
   var result = await controller.Run("timeline 1");
   if (!result.CommandSuccessful)
   {
      MessageBox.Show(result.Error.ToString());
   }
}
</code></pre>
