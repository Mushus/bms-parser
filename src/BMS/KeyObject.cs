namespace BMS
{
    class KeyObject : Object<int[]>
    {
        public KeyObject(int measure, int channel, int[] data) : base(measure, channel, data) { }
    }
}