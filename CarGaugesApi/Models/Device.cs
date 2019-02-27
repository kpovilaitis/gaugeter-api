namespace CarGaugesApi.Models
{
    public class Device
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BluetoothAddress { get; set; }

        public Device() { }

        //public Device(string name, string bluetoothAddress) {
        //    Name = name;
        //    BluetoothAddress = bluetoothAddress;
        //}
    }
}
