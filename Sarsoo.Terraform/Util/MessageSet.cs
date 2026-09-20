// using Sarsoo.Terraform.MachineReadableUI;
//
// namespace Sarsoo.Terraform.Util;
//
// public class MessageSet
// {
//     private readonly Lock _messageLock = new();
//     private readonly Dictionary<string, List<TerraformMessage>> _messages = new();
//
//     public virtual void Add(TerraformMessage message)
//     {
//         lock (_messageLock)
//         {
//             List<TerraformMessage> messages;
//             if (!_messages.TryGetValue(message.Type, out messages!))
//             {
//                 messages = new List<TerraformMessage>();
//                 _messages.Add(message.Type, messages);
//             }
//
//             messages.Add(message);
//         }
//     }
//
//     public IEnumerable<TerraformMessage> Get(string type)
//     {
//         lock (_messageLock)
//         {
//             return _messages[type].ToArray();
//         }
//     }
// }