

/////////////////////////Clients////////////////////////////////////////////////////

Javascript CDN
1- Run the Grunt configuration "compile" on the project which will generate the necessary documentation
	minification, and debug output.
2- Right click the project and hit Publish

Html Client Local Debug:
1- open the CMD prompt for the site's root directory
2- type "npm start" in the window
3- the browser window will open to the main page

Html Client Deployment
1- 

////////////////////////////////////////////////////////////////////////////////////



////Servers/////////////////////////////////////////////////////////////////////////


////////////Deploying NuGet package/////////////////////////////////////////////////

See the QueryAnalyzer.csproj file for the following statement, currently executes in the Release 
only build:

<Target Name="AfterBuild" Condition="'$(Configuration)' == 'Release'">
	<!--Publish a Nuget package on Release build
		See: https://docs.nuget.org/create/creating-and-publishing-a-package-->
	<Exec Command="nuget pack QueryAnalyzer.csproj -OutputDirectory ..\NugetPackages\  -Prop Configuration=Release">
	</Exec>
</Target>

Also the assembly version is changed through the Assembly.tt file in the project.

-----------------------------------------------------------------------------------

-----------------------------------------------------------------------------------

