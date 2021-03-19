#include "FastLED.h"
#define NUM_LEDS 4 
#define arraysize 11
CRGB leds[NUM_LEDS];
#define PIN 2 
 uint8_t Ary[arraysize];
String serialResponse = "";
void setup()
{
  FastLED.addLeds<WS2811, PIN, GRB>(leds, NUM_LEDS).setCorrection( TypicalLEDStrip );
  Serial.begin(115200);
  Ary[0]=0;
}

// *** REPLACE FROM HERE ***
void loop() {
    if (Serial.available())
    {
        serialResponse = Serial.readString();
        digitalWrite(LED_BUILTIN, HIGH);   
        delay(10);                       
        digitalWrite(LED_BUILTIN, LOW);         
        Serial.println(serialResponse);
        for(int m = 0; m < arraysize; m++)  
        {
          Ary[m]=0;
        }
        uint8_t i=0, j=0;
        while ( j<serialResponse.length()) {
          if (serialResponse.charAt(j)==';') {
            i++;
          }
          else {
            Ary[i]=Ary[i]*10;
            Ary[i]=Ary[i]+(serialResponse.charAt(j)-'0');
          }
          j++;
        }
    }
  switch(Ary[0]) {
    
    case 1  : {
                // RGBLoop - no parameters
                //mehet
                RGBLoop();
                break;
              }

    case 2  : {
                // FadeInOut - Color (red, green. blue)
                //mehett
                FadeInOut(Ary[1], Ary[2], Ary[3]); // red
                FadeInOut(Ary[4], Ary[5], Ary[6]); // white 
                FadeInOut(Ary[7], Ary[8], Ary[9]); // blue
                break;
              }
              
    case 3  : {
                // Strobe - Color (red, green, blue), number of flashes, flash speed, end pause
                //mehet
                Strobe(Ary[1], Ary[2], Ary[3], Ary[4], Ary[5], Ary[6]);
                break;
              }

              
    case 4  : {
                // Sparkle - Color (red, green, blue), speed delay
                //csillogas mehet
                Sparkle(Ary[1], Ary[2], Ary[3], Ary[4]);
                break;
              }
               
    case 5  : {
                // SnowSparkle - Color (red, green, blue), sparkle delay, speed delay
                //ho, mehett
                SnowSparkle(Ary[1], Ary[2], Ary[3], Ary[4], random(100,Ary[5]));
                break;
              }
              
    case 6 : {
                // Running Lights - Color (red, green, blue), wave dealy
                //futo rgb mehet
                RunningLights(Ary[1],Ary[2],Ary[3], Ary[10]);  // red
                RunningLights(Ary[4],Ary[5],Ary[6], Ary[10]);  // white
                RunningLights(Ary[7],Ary[8],Ary[9], Ary[10]);  // blue
                break;
              }
              
    case 7 : {
                // colorWipe - Color (red, green, blue), speed delay
                //visssza futo rgb mehet
                colorWipe(Ary[1],Ary[2],Ary[3], Ary[4]);
                colorWipe(0x00,0x00,0x00, Ary[4]);
                break;
              }

    case 8 : {
                // rainbowCycle - speed delay
                //mehet
                rainbowCycle(Ary[1]);
                break;
              }

    case 9 : {
                // theatherChase - Color (red, green, blue), speed delay
                //villogo futo mehet
                theaterChase(Ary[1],Ary[2],Ary[3],Ary[4]);
                break;
              } 

    case 10 : {
                // theaterChaseRainbow - Speed delay
                //villogo futo mehet rgb
                theaterChaseRainbow(Ary[1]);
                break;
              }

    case 11 : {
                // Fire - Cooling rate, Sparking rate, speed delay
                //tuzes valami mehett
                Fire(Ary[1],Ary[2],Ary[3]);
                break;
              }


              // simple bouncingBalls not included, since BouncingColoredBalls can perform this as well as shown below
              // BouncingColoredBalls - Number of balls, color (red, green, blue) array, continuous
              // CAUTION: If set to continuous then this effect will never stop!!! 
              
    case 12 : {
                // mimic BouncingBalls
                //golyo mehet
                byte onecolor[1][3] = { {Ary[1], Ary[2], Ary[3]} };
                BouncingColoredBalls(1, onecolor, false);
                break;
              }

    case 13 : {
                // multiple colored balls
                //mehet
                byte colors[3][3] = { {Ary[1], Ary[2], Ary[3]}, 
                                      {Ary[4], Ary[5], Ary[6]}, 
                                      {Ary[7], Ary[8], Ary[9]} };
                BouncingColoredBalls(3, colors, false);
                break;
              }

    case 14 : {
                // meteorRain - Color (red, green, blue), meteor size, trail decay, random trail decay (true/false), speed delay 
                //mehet
                meteorRain(Ary[1],Ary[2],Ary[3],Ary[4], Ary[5], Ary[6], Ary[7]);
                break;
              }

    case 0 : {
        setAll(0, 0, 0);
                break;
                }
  }
}




// *************************
// ** LEDEffect Functions **
// *************************

void RGBLoop(){
  for(int j = 0; j < 3; j++ ) { 
    // Fade IN
    for(int k = 0; k < 256; k++) { 
      switch(j) { 
        case 0: setAll(k,0,0); break;
        case 1: setAll(0,k,0); break;
        case 2: setAll(0,0,k); break;
      }
      showStrip();
      delay(3);
    }
    // Fade OUT
    for(int k = 255; k >= 0; k--) { 
      switch(j) { 
        case 0: setAll(k,0,0); break;
        case 1: setAll(0,k,0); break;
        case 2: setAll(0,0,k); break;
      }
      showStrip();
      delay(3);
    }
  }
}

void FadeInOut(byte red, byte blue, byte green){
  float r, g, b;
      
  for(int k = 0; k < 256; k=k+1) {
    delay(10); 
    r = (k/256.0)*red;
    g = (k/256.0)*green;
    b = (k/256.0)*blue;
    setAll(r,g,b);
    showStrip();
  }
     
  for(int k = 255; k >= 0; k=k-2) {
    delay(10);
    r = (k/256.0)*red;
    g = (k/256.0)*green;
    b = (k/256.0)*blue;
    setAll(r,g,b);
    showStrip();
  }
}

void Strobe(byte red, byte green, byte blue, int StrobeCount, int FlashDelay, int EndPause){
  for(int j = 0; j < StrobeCount; j++) {
    setAll(red,green,blue);
    showStrip();
    delay(FlashDelay);
    setAll(0,0,0);
    showStrip();
    delay(FlashDelay);
  }
 
 delay(EndPause);
}

void Sparkle(byte red, byte green, byte blue, int SpeedDelay) {
  int Pixel = random(NUM_LEDS);
  setPixel(Pixel,red,green,blue);
  showStrip();
  delay(SpeedDelay);
  setPixel(Pixel,0,0,0);
}

void SnowSparkle(byte red, byte green, byte blue, int SparkleDelay, int SpeedDelay) {
  setAll(red,green,blue);
  
  int Pixel = random(NUM_LEDS);
  setPixel(Pixel,0xff,0xff,0xff);
  showStrip();
  delay(SparkleDelay);
  setPixel(Pixel,red,green,blue);
  showStrip();
  delay(SpeedDelay);
}

void RunningLights(byte red, byte green, byte blue, int WaveDelay) {
  int Position=0;
  
  for(int i=0; i<NUM_LEDS*2; i++)
  {
      Position++; // = 0; //Position + Rate;
      for(int i=0; i<NUM_LEDS; i++) {
        // sine wave, 3 offset waves make a rainbow!
        //float level = sin(i+Position) * 127 + 128;
        //setPixel(i,level,0,0);
        //float level = sin(i+Position) * 127 + 128;
        setPixel(i,((sin(i+Position) * 127 + 128)/255)*red,
                   ((sin(i+Position) * 127 + 128)/255)*green,
                   ((sin(i+Position) * 127 + 128)/255)*blue);
      }
      
      showStrip();
      delay(WaveDelay);
  }
}

void colorWipe(byte red, byte green, byte blue, int SpeedDelay) {
  for(uint16_t i=0; i<NUM_LEDS; i++) {
      setPixel(i, red, green, blue);
      showStrip();
      delay(SpeedDelay);
  }
}

void rainbowCycle(int SpeedDelay) {
  byte *c;
  uint16_t i, j;

  for(j=0; j<256*1; j++) { // 5 cycles of all colors on wheel
    for(i=0; i< NUM_LEDS; i++) {
      c=Wheel(((i * 256 / NUM_LEDS) + j) & 255);
      setPixel(i, *c, *(c+1), *(c+2));
    }
    showStrip();
    delay(SpeedDelay);
  }
}

// used by rainbowCycle and theaterChaseRainbow
byte * Wheel(byte WheelPos) {
  static byte c[3];
  
  if(WheelPos < 85) {
   c[0]=WheelPos * 3;
   c[1]=255 - WheelPos * 3;
   c[2]=0;
  } else if(WheelPos < 170) {
   WheelPos -= 85;
   c[0]=255 - WheelPos * 3;
   c[1]=0;
   c[2]=WheelPos * 3;
  } else {
   WheelPos -= 170;
   c[0]=0;
   c[1]=WheelPos * 3;
   c[2]=255 - WheelPos * 3;
  }

  return c;
}

void theaterChase(byte red, byte green, byte blue, int SpeedDelay) {
  for (int j=0; j<10; j++) {  //do 10 cycles of chasing
    for (int q=0; q < 3; q++) {
      for (int i=0; i < NUM_LEDS; i=i+3) {
        setPixel(i+q, red, green, blue);    //turn every third pixel on
      }
      showStrip();
     
      delay(SpeedDelay);
     
      for (int i=0; i < NUM_LEDS; i=i+3) {
        setPixel(i+q, 0,0,0);        //turn every third pixel off
      }
    }
  }
}

void theaterChaseRainbow(int SpeedDelay) {
  byte *c;
  
  for (int j=0; j < 256; j++) {     // cycle all 256 colors in the wheel
    for (int q=0; q < 3; q++) {
        for (int i=0; i < NUM_LEDS; i=i+3) {
          c = Wheel( (i+j) % 255);
          setPixel(i+q, *c, *(c+1), *(c+2));    //turn every third pixel on
        }
        showStrip();
       
        delay(SpeedDelay);
       
        for (int i=0; i < NUM_LEDS; i=i+3) {
          setPixel(i+q, 0,0,0);        //turn every third pixel off
        }
    }
  }
}

void Fire(int Cooling, int Sparking, int SpeedDelay) {
  static byte heat[NUM_LEDS];
  int cooldown;
  
  // Step 1.  Cool down every cell a little
  for( int i = 0; i < NUM_LEDS; i++) {
    cooldown = random(0, ((Cooling * 10) / NUM_LEDS) + 2);
    
    if(cooldown>heat[i]) {
      heat[i]=0;
    } else {
      heat[i]=heat[i]-cooldown;
    }
  }
  
  // Step 2.  Heat from each cell drifts 'up' and diffuses a little
  for( int k= NUM_LEDS - 1; k >= 2; k--) {
    heat[k] = (heat[k - 1] + heat[k - 2] + heat[k - 2]) / 3;
  }
    
  // Step 3.  Randomly ignite new 'sparks' near the bottom
  if( random(255) < Sparking ) {
    int y = random(7);
    heat[y] = heat[y] + random(160,255);
    //heat[y] = random(160,255);
  }

  // Step 4.  Convert heat to LED colors
  for( int j = 0; j < NUM_LEDS; j++) {
    setPixelHeatColor(j, heat[j] );
  }

  showStrip();
  delay(SpeedDelay);
}

void setPixelHeatColor (int Pixel, byte temperature) {
  // Scale 'heat' down from 0-255 to 0-191
  byte t192 = round((temperature/255.0)*191);
 
  // calculate ramp up from
  byte heatramp = t192 & 0x3F; // 0..63
  heatramp <<= 2; // scale up to 0..252
 
  // figure out which third of the spectrum we're in:
  if( t192 > 0x80) {                     // hottest
    setPixel(Pixel, 255, 255, heatramp);
  } else if( t192 > 0x40 ) {             // middle
    setPixel(Pixel, 255, heatramp, 0);
  } else {                               // coolest
    setPixel(Pixel, heatramp, 0, 0);
  }
}

void BouncingColoredBalls(int BallCount, byte colors[][3], boolean continuous) {
  float Gravity = -9.81;
  int StartHeight = 1;
  
  float Height[BallCount];
  float ImpactVelocityStart = sqrt( -2 * Gravity * StartHeight );
  float ImpactVelocity[BallCount];
  float TimeSinceLastBounce[BallCount];
  int   Position[BallCount];
  long  ClockTimeSinceLastBounce[BallCount];
  float Dampening[BallCount];
  boolean ballBouncing[BallCount];
  boolean ballsStillBouncing = true;
  
  for (int i = 0 ; i < BallCount ; i++) {   
    ClockTimeSinceLastBounce[i] = millis();
    Height[i] = StartHeight;
    Position[i] = 0; 
    ImpactVelocity[i] = ImpactVelocityStart;
    TimeSinceLastBounce[i] = 0;
    Dampening[i] = 0.90 - float(i)/pow(BallCount,2);
    ballBouncing[i]=true; 
  }

  while (ballsStillBouncing) {
    for (int i = 0 ; i < BallCount ; i++) {
      TimeSinceLastBounce[i] =  millis() - ClockTimeSinceLastBounce[i];
      Height[i] = 0.5 * Gravity * pow( TimeSinceLastBounce[i]/1000 , 2.0 ) + ImpactVelocity[i] * TimeSinceLastBounce[i]/1000;
  
      if ( Height[i] < 0 ) {                      
        Height[i] = 0;
        ImpactVelocity[i] = Dampening[i] * ImpactVelocity[i];
        ClockTimeSinceLastBounce[i] = millis();
  
        if ( ImpactVelocity[i] < 0.01 ) {
          if (continuous) {
            ImpactVelocity[i] = ImpactVelocityStart;
          } else {
            ballBouncing[i]=false;
          }
        }
      }
      Position[i] = round( Height[i] * (NUM_LEDS - 1) / StartHeight);
    }

    ballsStillBouncing = false; // assume no balls bouncing
    for (int i = 0 ; i < BallCount ; i++) {
      setPixel(Position[i],colors[i][0],colors[i][1],colors[i][2]);
      if ( ballBouncing[i] ) {
        ballsStillBouncing = true;
      }
    }
    
    showStrip();
    setAll(0,0,0);
  }
}

void meteorRain(byte red, byte green, byte blue, byte meteorSize, byte meteorTrailDecay, boolean meteorRandomDecay, int SpeedDelay) {  
  setAll(0,0,0);
  
  for(int i = 0; i < NUM_LEDS+NUM_LEDS; i++) {
    // fade brightness all LEDs one step
    for(int j=0; j<NUM_LEDS; j++) {
      if( (!meteorRandomDecay) || (random(10)>5) ) {
        fadeToBlack(j, meteorTrailDecay );        
      }
    }
    // draw meteor
    for(int j = 0; j < meteorSize; j++) {
      if( ( i-j <NUM_LEDS) && (i-j>=0) ) {
        setPixel(i-j, red, green, blue);
      } 
    }
   
    showStrip();
    delay(SpeedDelay);
  }
}

// used by meteorrain
void fadeToBlack(int ledNo, byte fadeValue) {
   leds[ledNo].fadeToBlackBy( fadeValue );
}

// *** REPLACE TO HERE ***



// ***************************************
// ** FastLed/NeoPixel Common Functions **
// ***************************************

// Apply LED color changes
void showStrip() {
   FastLED.show();
}

// Set a LED color (not yet visible)
void setPixel(int Pixel, byte red, byte green, byte blue) {
   leds[Pixel].r = red;
   leds[Pixel].g = green;
   leds[Pixel].b = blue;
}

// Set all LEDs to a given color and apply it (visible)
void setAll(byte red, byte green, byte blue) {
  for(int i = 0; i < NUM_LEDS; i++ ) {
    setPixel(i, red, green, blue); 
  }
  showStrip();
}
