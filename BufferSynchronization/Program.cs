

//  
// using (BufferedStream sw = new BufferedStream(new MemoryStream(), 64)) {
//     sw.Write();
// }
// var test = "tes"; 

/* 
Google Bard Questions: 
1- how do streams communicate// using (BufferedStream sw = new BufferedStream(new MemoryStream(), 64)) {
2- how to maintain a lock on the buffer if it's full until we release it ? 
*/ 

// ProducerStream -----> [Buffer] -------> consumerStream 
Stream producerStream = new FileStream("./test.json", FileMode.OpenOrCreate); 
BufferAbstraction ba = new BufferAbstraction(4000);

ba.WriteBytes(producerStream);


// Stream consumerStream = new FileStream("./test.json", FileMode.OpenOrCreate);
// consumerStream.Read(ba.Buffer, 0, Int32.MaxValue); 

