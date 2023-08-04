# Blazor Photo Library
This is a photo library built in .NET 7, Blazor, and Bootstrap 5.1.0. This was built in consideration of the technical showcase scenario provided by Lean Techniques.

This app consists of a single page that has a dropdown menu for selecting the album ID. Upon selecting the album ID, the photos tied to that album will be loaded.

## Build
### Build in Visual Studio
1. Clone the repository.
2. Open BlazorPhotoLibrary.sln in Visual Studio 2022
3. Select BlazorPhotoLibrary.proj as your startup project.
4. Go to Build > Build Solution to build the project
   - Alternatively go to Debug > Start Debugging to launch the app in debug mode.
### Build via CLI
* Clone the repository.
* Open the command line and `cd` to the solution folder.
* Execute `dotnet build` to build the solution.

## Future Considerations
If I were to continue building this application, I would consider implementing a number of things including:
- IA robust retry system for contacting the repository
- A caching mechanism like Redis
- A logging mechanism
- Further unit testing & automation tests