namespace app.shoppingpromotions.infrastructure
{
    public class MemcachedOptions
    {
        public List<MemcachedServerOptions> Servers { get; set; }
    }

    public class MemcachedServerOptions
    {
        public string Address { get; set; }
        public int Port { get; set; }
    }
}