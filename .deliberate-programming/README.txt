# HOW TO


## Prepare

Copy this contents of this folder to the root of your Git repo (or the root of the folder tree you're working under).

Then check that everything is working by first running this test inside the folder in a console/terminal/bash window:

MAC/LINUX
---------
mono repeat.exe 5 bash -c ./hello.sh

WINDOWS
-------
repeat.exe 5 cmd.exe /c .\hello.bat

The output should be a "hello, world!" message appearing every 5 seconds.


## Use

To actually use repeat.exe to automatically commit changes you need to do the following:

1. Open a console/terminal/bash window on the parent folder, the root of your repo/working tree.
2. Start auto committing like this:

MAC/LINUX
---------
mono .deliberate-programming/repeat.exe 60 bash -c .deliberate-programming/dp-commit.sh

WINDOWS
-------
mono .deliberate-programming\repeat.exe 60 cmd.exe /c .deliberate-programming\dp-commit.bat


If you like, do a quick test by first running repeat.exe with a period of 5 seconds instead of 60.


