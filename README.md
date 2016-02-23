# wt-betty
War Thunder's Betty. Cockpit warning sounds for simulator pilots.

========================
Before You Start
========================
This software is currently BETA and I am the only tester so there may be bugs. Please feel free to report any bugs.

========================
Bitching Betty
========================
Source: https://www.wikiwand.com/en/Bitching_Betty

Bitching Betty is a slang term used by some pilots and aircrew (mainly North American), when referring to the voices used by some aircraft warning systems.

The name "Betty" is a generic popular traditional name from American culture, and is thought not to be derived from its more recent uses to describe an attractive female (in reference to Betty Boop or Betty Rubble of The Flintstones).

The enunciating voice, in at least some aircraft systems, may be either male or female and in some cases this may be selected according to pilot preference. If the voice is female it may be referred to as Bitching Betty; if the voice is male it may be referred to as Barking Bob. A female voice is heard on military aircraft such as the F-16 Fighting Falcon, the Eurofighter Typhoon and the Mikoyan MiG-29. A male voice is heard on Boeing commercial airliners and is also used in the BAE Hawk.

========================
War Thunder's Betty
========================
War Thunder's Betty is an add-on utility for Gaijin's War Thunder. The aim of the utility is to help simulator pilots by audible warnings as desktop pilots lack the real sense of flight and a physical cockpit environment. 

The software do not change anything in the core files of War Thunder but it simply reads the indicators and states of the flight from  http://localhost:8111 and uses those values to operate.

========================
Installation
========================
1. Download
2. Unzip
3. Execute the wt-betty.exe executable (for example you may double click if you like ;)).

========================
How to use it
========================
Just run the software and it will begin listening for a valid flight and will start automatically when you jump into a flight. You dont have to worry about it. Just let it do its job in the background. But if you like you can still start/stop manually.

========================
Behaviour
========================
Currently there are 3 annunciator sounds.

1. Over G warning
2. Angle of Attack warning
3. Gear Warning (Gear Up & Gear Down)

1. Over-G warning annunciator will be activated when the G-Force exceeds the given threshold then Betty will call out "Over G" repeatedly. The default threshold is "6 G's", but the user can change it from options tab and save. 

2. Angle of Attack warning annunciator will be activated when Angle of Attack exceeds the given threshold then Betty will call out a "Stall Horn" which means you are about to stall your aircraft. Stalls can happen at any speed if you pull too hard on the stick. The Angle of Attack annuciator will be available if the gears are retracted so you will not hear anything if you are about to stall in a landing configuration (gears down position). Default threshold is 12 degrees.

3. Gear warning annunciator will be activated when you forget to gear up or gear down. While airborne if you forget to gear up at speeds greater than 250 km/h Betty will call "Gear up" and if you forget to gear down when you are about to land with flaps at landing configuration below 210 km/h.

I believe it is a better practice to warn the pilot earlier so thresholds are a little bit lower than the actual limits.

If you like you can enable or disable the annunciators by clicking the checkboxes next to them at options. Default they will be all enabled.

========================
Options
========================
Checboxes: Enable/Disable
Sliders: Change Thresholds
Text Boxes: Just edit them

========================
Aircraft Flight Models
========================
Not all aircraft are same. They have different performance. This software will not and can not calculate dynamic performance data for particular flight models. They are just simple thresholds. Therefore you have the option to assign your own values and save them to your preferences from the options tab.

========================
Known Issues
========================
The utility may not perform properly with the bombers as there is no indicator values present for bomber types.

========================
License
========================
This software is free to use, free to copy, free to share. You can do whatever you want. Just put credits to me for God sake.

Copyright (c) 2016 Ahmet 'Europa' Mehmetbeyoglu
Contact me in the War Thunder forums. 