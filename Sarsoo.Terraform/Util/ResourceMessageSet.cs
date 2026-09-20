// using Sarsoo.Terraform.MachineReadableUI;
//
// namespace Sarsoo.Terraform.Util;
//
// public class ResourceMessageSet: MessageSet
// {
//     private readonly Lock _messageLock = new();
//     private readonly Dictionary<string, List<TerraformMessage>> _messages = new();
//
//     public override void Add(TerraformMessage message)
//     {
//         base.Add(message);
//
//         lock (_messageLock)
//         {
//             if (message is { Change.Resource.Address: { } raddr })
//             {
//                 List<FullMessage> messages;
//                 if (!_messages.TryGetValue(raddr, out messages!))
//                 {
//                     messages = new List<FullMessage>();
//                     _messages.Add(raddr, messages);
//                 }
//
//                 messages.Add(message);
//             }
//         }
//     }
//
//
//     public IDictionary<string, TerraformMessage[]> Get()
//     {
//         lock (_messageLock)
//         {
//             return _messages.Select(x => (x.Key, x.Value.ToArray())).ToDictionary();
//         }
//     }
// }