
// ProducerStream -----> [Buffer] -------> consumerStream 
string ProducerFilePath = "./producer.txt"; 
string ConsumerFilePath = "./consumer.txt"; 
Stream producerStream = new FileStream(ProducerFilePath, FileMode.OpenOrCreate); 
Stream consumerStream = new FileStream(ConsumerFilePath, FileMode.OpenOrCreate);
BufferAbstraction ba = new BufferAbstraction(4000);
// producer -> [Buffer] -> consumer
ba.WriteBytes(producerStream); 
ba.ReadBytes(consumerStream);
