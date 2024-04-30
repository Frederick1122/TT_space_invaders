using UnityEngine;

namespace Rx
{
    public class MessageBase {
        public MonoBehaviour sender {get; private set;} 
        public int id {get; private set;} 
        public System.Object data {get; private set;} 

        public MessageBase (MonoBehaviour sender, int id, System.Object data) {
            this.sender = sender;
            this.id = id;
            this.data = data;
        }
        
        public static MessageBase Create (MonoBehaviour sender,
            int id, System.Object data = null) {
            return new MessageBase (sender, id, data);
        }
    }   
}