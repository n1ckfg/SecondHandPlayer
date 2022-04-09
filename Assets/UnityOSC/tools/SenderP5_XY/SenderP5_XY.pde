//based on oscP5parsing by andreas schlegel

import oscP5.*;
import netP5.*;

String ipNumber = "127.0.0.1";
int receivePort = 9000;
int sendPort = 7110;
OscP5 oscP5;
NetAddress myRemoteLocation;
//---
String[] oscChannelNames = { "pos" };
PVector pos = new PVector(0,0,0);
boolean sendOsc = true;

void setup(){
size(640, 480, P2D);
smooth();
oscSetup();
}

void draw(){
  background(0);
  int s = 10;
  if (mousePressed) {
    pos = getMousePos(true);
    oscSend();
    s = 20;
    fill(255,0,0);
  } else {
    fill(127,0,0);
  }
  ellipse(mouseX, mouseY, s, s);
}

void mouseReleased() {
  pos = getMousePos(false);
  oscSend();
}

PVector getMousePos(boolean b) {
  return new PVector(float(mouseX)/float(width), float(mouseY)/float(height), float(int(b)));
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

//1.  This function initializes OSC.  Put it in your setup().
void oscSetup() {
  oscP5 = new OscP5(this,receivePort);  // start osc
  myRemoteLocation = new NetAddress(ipNumber,sendPort);
}

//2.  This function receives OSC.  It runs automatically; no need to call it in your code.
void oscEvent(OscMessage myMessage) {
  println(myMessage);
  for(int i=0;i<oscChannelNames.length;i++){
  if(myMessage.checkAddrPattern("/" + oscChannelNames[i])) {
    if(myMessage.checkTypetag("f")) {  // types are i = int, f = float, s = String, ifs = all
      //oscReceiveData[i] = myMessage.get(0).floatValue();  // commands are intValue, floatValue, stringValue
    }  
  }
}
}

//3.  This function sends OSC.  Put it in your draw(), or in control functions like mousePressed() and keyPressed().
void oscSend(){
    //--
  if(sendOsc){
    OscMessage myMessage;
  
    myMessage = new OscMessage("/" + oscChannelNames[0]);
    myMessage.add(pos.x);
    myMessage.add(pos.y);
    myMessage.add(pos.z);
    oscP5.send(myMessage, myRemoteLocation);
  }
}

//4.  This function uses incoming OSC data in your sketch by assigning it to variables.
void oscUse(){
//println("posX: " + oscReceiveData[0] + "   posY: " + oscReceiveData[1] + "   posZ: " + oscReceiveData[2]);
}

//END
