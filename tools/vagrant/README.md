### Starting DynamoWebServer with a Vagrant Linux VM on Windows

#### Prerequisites

- Vagrant 
- Oracle VirtualBox
- An SSH client for Windows
- A build of LibG in the LibG folder, one directory up from Dynamo (contact Peter for access)
 
#### Instructions 

From an SSH capable terminal such as MINGW:

1.  `cd` to tools/vagrant directory
2.  `vagrant up`
3.  `vagrant ssh`

From the VM SSH session:

4.  `sudo apt-get install aptitude`
5.  `sudo aptitude install libboost-thread1.55-dev`
6.  `sudo aptitude install mono-complete`
7.  `cd /dynamo` (You're now in the Dynamo binary directory)
8.  `mono DynamoWebServer.exe`

Config files:

- Set ProtoGeometry.config file to point to libg_220/LibG.ProtoInterface.dll.  We don't support 219 on Linux.
- Set DynamoWebServer.exe.config to your bound IP on VirtualBox, not loopback (127.0.0.1).  This can be obtained by running the ifconfig command and looking at the inet addr.

