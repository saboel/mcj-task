# mcj-task

**Introduction:**
An AGV project needs an interface between the middleware and the customers host system. The
middleware already handles all processes for controlling the transport orders. The customer host system
provides a text file with information about the delivery station for the load.
Various picking stations in the warehouse are available for the AGV to pick up the load. A hardware IO
signal triggers the start of a transport. The barcode reader at the AGV reads the ID of the load during
the loading operation. Then the AGV waits for the target information where the load has to be delivered
to. If the middleware cannot provide a target, the load must be send to the clearing station.

**Programming Task**:
The aim is to create a runnable program with a gui that reads the Load ID and outputs the target ID for the load

**Approach**:
I took a simple approach to solve the problem at hand. I utilized Window forms for the graphical user interface and 
threw the latest file contents into a hashmap using the filename -> file contents. I realized there is more clever memory efficient ways of solving it, 
for example utilizing the loadID the user enters and mapping that to the correct target. Instead of storing the entire file contents into memory we can extract the requried information (target id) directly from the latest file. 

**Core of the program logic**: 
The form1.cs file is the main logic of the entire task. 
