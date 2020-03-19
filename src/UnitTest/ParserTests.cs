using Microsoft.VisualStudio.TestTools.UnitTesting;
using BMS;
using System.Text;
using System.IO;

namespace BMS.Tests
{
    [TestClass()]
    public class ParserTests
    {
        [TestMethod()]
        public void ParserTest()
        {
            var text = @"
*---------------------- HEADER FIELD
#PLAYER 2
#GENRE Liquid Funk
#TITLE H2O
#ARTIST Mushus
#BPM 170
#MIDIFILE h2o.midi
#PLAYLEVEL 1
#RANK 1
#VOLWAV 90
#TOTAL 0.1
#WAV01 01.wav
#BMP11 11.bmp
*---------------------- MAIN DATA FIELD
#00111:01010101
#00112:00010001
#00113:010101
";
            var b = Encoding.ASCII.GetBytes(text);
            var s = new MemoryStream(b);

            // how to use
            var parser = new Parser();
            var bms = parser.Parse(s);

            // assertion
            Assert.AreEqual(Player.Couple, bms.Player);
            Assert.AreEqual("Liquid Funk", bms.Genre);
            Assert.AreEqual("H2O", bms.Title);
            Assert.AreEqual("Mushus", bms.Artist);
            Assert.AreEqual(170, bms.BPM);
            Assert.AreEqual("h2o.midi", bms.MidiFile);
            Assert.AreEqual(1, bms.PlayLevel);
            Assert.AreEqual(Rank.Hard, bms.Rank);
            Assert.AreEqual(90, bms.VolWav);
            Assert.AreEqual(0.1, bms.Total);

            string wav;
            bms.Wav.TryGetValue(1, out wav);
            Assert.AreEqual("01.wav", wav);

            string bmp;
            // 37 -> 36進数の11
            bms.Bmp.TryGetValue(37, out bmp);
            Assert.AreEqual("11.bmp", bmp);

            var firstObj = bms.Objects[0];
            Assert.AreEqual(1, firstObj.Measure);
            Assert.AreEqual(11, firstObj.Channel);

            var events = bms.Timeline.Events();
            Assert.AreEqual(11, events[0].Channel);
            Assert.AreEqual(1412, events[0].Millisecond);
            Assert.AreEqual(1, events[0].Reference);
            Assert.AreEqual(13, events[1].Channel);
            Assert.AreEqual(1412, events[1].Millisecond);
            Assert.AreEqual(1, events[1].Reference);
            Assert.AreEqual(11, events[2].Channel);
            Assert.AreEqual(1765, events[2].Millisecond);
            Assert.AreEqual(1, events[2].Reference);
            Assert.AreEqual(12, events[3].Channel);
            Assert.AreEqual(1765, events[3].Millisecond);
            Assert.AreEqual(1, events[3].Reference);
            Assert.AreEqual(13, events[4].Channel);
            Assert.AreEqual(1882, events[4].Millisecond);
            Assert.AreEqual(1, events[4].Reference);
            Assert.AreEqual(11, events[5].Channel);
            Assert.AreEqual(2118, events[5].Millisecond);
            Assert.AreEqual(1, events[5].Reference);
            Assert.AreEqual(13, events[6].Channel);
            Assert.AreEqual(2353, events[6].Millisecond);
            Assert.AreEqual(1, events[6].Reference);
            Assert.AreEqual(11, events[7].Channel);
            Assert.AreEqual(2471, events[7].Millisecond);
            Assert.AreEqual(1, events[7].Reference);
            Assert.AreEqual(12, events[8].Channel);
            Assert.AreEqual(2471, events[8].Millisecond);
            Assert.AreEqual(1, events[8].Reference);
        }
    }
}