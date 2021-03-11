# Example Application for Object Detection in C# with YOLO v4

## How to compile and run
### Requirements
For this application to run, you **need to have a NVIDIA GPU and it's drivers installed**. Then you have to install following dependencies: </br>
- [NVIDIA CUDA **10.2**](https://developer.nvidia.com/cuda-10.2-download-archive) (version is very important!)
- [NVIDIA CUDNN **7.6.5** for CUDA 10.2](https://developer.nvidia.com/rdp/cudnn-archive)
- Copy the cudnn64_7.dll from the output directory of the CUDA installation into the `<ProjectDir>/CronnBlogProject/bin/Debug/netcoreapp3.1` folder
- [Microsoft Visual C++ Redistributable vor VS 2015, 2017 and 2019 x64](https://github.com/AlturosDestinations/Alturos.Yolo/files/4744207/vcredist_x64.zip)
### Compile and run
When you finished installing all requirements, you start up the solution (.sln) file in Visual Studio and build the application once.
After that, you copy all files from the `assets` folder and the ones that you can download [here](INSTER LINK) to the same directory as the `CronnBlogProject.exe` file.
Now you are done and can try it out yourself.

## Demo video
If you want to see this application running but don't want to build it yourself, you can watch the `ObjectDetectionDemo.mp4` video.