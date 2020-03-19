# bms-parser

BMS parser written in C#

## Usage

```
var s = File.Open("test.bms", FileMode.Open)
var parser = new Parser();
var bms = parser.Parse(s);

Console.WriteLine(bms.Title); // show title
Console.WriteLine(bms.Timeline.Event); // show event data
```
