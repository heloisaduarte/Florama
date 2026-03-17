namespace Florama.Domain.Notifications
{
    public class DomainNotification
    {
        public string Key { get; }
        public string Value { get; }
        public DateTime DataOcorrencia { get; private set; }

        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
            DataOcorrencia = DateTime.Now;
        }
    }
}
