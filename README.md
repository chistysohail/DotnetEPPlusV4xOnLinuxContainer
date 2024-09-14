DotnetEPPlusV4xOnLinuxContainer

This project is a .NET 6 console application that runs inside a Docker container. The container is set up to handle System.Drawing operations using libgdiplus, which is required for drawing and image manipulation in Linux environments.

Prerequisites
Docker installed on your machine.
.NET 6 SDK (for development purposes, if needed).
Build and Run the Docker Image

1. Build the Docker Image
To build the Docker image, run the following command from the root directory (where DotnetEPPlusV4xOnLinuxContainer.sln is located):
docker build -t dotnetepplusv4xonlinuxcontainer -f DotnetEPPlusV4xOnLinuxContainer/Dockerfile .
This command will:

Use the .NET SDK image to build the project.
Install libgdiplus for handling System.Drawing in Linux.
Create a runtime image that includes the application.

2. Run the Docker Container
Once the image is built, run the container using the following command:
docker run --rm -it dotnetepplusv4xonlinuxcontainer
This will:
Start the container.Run the DotnetEPPlusV4xOnLinuxContainer.dll application.

3. Troubleshooting: Check for libgdiplus
If you encounter issues with System.Drawing, ensure that libgdiplus is installed correctly. You can check if it's installed in the container by running:
docker run -it --entrypoint /bin/bash dotnetepplusv4xonlinuxcontainer

Inside the container, use this command to find the libgdiplus.so file:
find / -name "libgdiplus.so"

Additional Information
libgdiplus: This library is required for System.Drawing functionality in Linux-based environments. It is installed during the Docker build process.
Multi-stage Dockerfile: This Dockerfile uses a multi-stage build to keep the final container lightweight by including only the necessary runtime files.


4. Error/Issue faced initially:
>docker run 18bf7c1f3
Unhandled exception. System.TypeInitializationException: The type initializer for 'Gdip' threw an exception.
 ---> System.DllNotFoundException: Unable to load shared library 'libgdiplus' or one of its dependencies. In order to help diagnose loading problems, consider setting the LD_DEBUG environment variable: liblibgdiplus: cannot open shared object file: No such file or directory
   at System.Drawing.SafeNativeMethods.Gdip.GdiplusStartup(IntPtr& token, StartupInput& input, StartupOutput& output)
   at System.Drawing.SafeNativeMethods.Gdip..cctor()
   --- End of inner exception stack trace ---
   at System.Drawing.SafeNativeMethods.Gdip.GdipGetGenericFontFamilySansSerif(IntPtr& fontfamily)
   at System.Drawing.FontFamily.GetGdipGenericSansSerif()
   at System.Drawing.FontFamily.get_GenericSansSerif()
   at System.Drawing.Font.CreateFont(String familyName, Single emSize, FontStyle style, GraphicsUnit unit, Byte charSet, Boolean isVertical)
   at System.Drawing.Font..ctor(String familyName, Single emSize, FontStyle style, GraphicsUnit unit, Byte gdiCharSet, Boolean gdiVerticalFont)
   at OfficeOpenXml.ExcelRangeBase.AutoFitColumns(Double MinimumWidth, Double MaximumWidth)
   at OfficeOpenXml.ExcelRangeBase.AutoFitColumns(Double MinimumWidth)
   at OfficeOpenXml.ExcelRangeBase.AutoFitColumns()
   at Program.Main() in /src/DotnetEPPlusV4xOnLinuxContainer/Program.cs:line 60
>
>
