# 背景介绍
一般在线购物商城都会遇到如团购、秒杀之类的活动，而这样的活动有一个共同的特点就是访问量激增、上千甚至上万人抢购一个商品。然而，作为活动商品，库存肯定是很有限的，如何控制库存不让出现超买，以防止造成不必要的损失是众多电子商务网站程序员头疼的问题，这同时也是最基本的问题。
</br></br>服务器在处理并发请求时我们的程序是多线程执行的，当一个线程T1拿到一个公共资源A进行计算，正要将计算结果重新赋值给公共资源A时，系统将CPU的使用权分配给另一个线程T2（线程T1将处于等待CPU状态），T2线程在执行的过程中将公共资源A修改，当线程T1重新获得CPU的执行权时继续执行赋值操作，
最后可能会得到一个错误的A值。
</br></br>通常我们解决此类问题可以使用线程锁，让线程在执行扣减库存的代码上下文中同步执行，但是使用锁也会遇到很多问题，特别是在分布式微服务等场景下处理各种锁的问题是让人十分头痛的一件事。

# Actor模型
Actor是计算机科学领域中的一个并行计算模型，它把actors当做通用的并行计算原语：一个actor对接收到的消息做出响应，进行本地决策，可以创建更多的actor，或者发送更多的消息；同时准备接收下一条消息。
在Actor理论中，一切都被认为是actor，这和面向对象语言里一切都被看成对象很类似。但包括面向对象语言在内的软件通常是顺序执行的，而Actor模型本质上则是并发的。
</br></br>
每个Actor都有一个(恰好一个)Mailbox。Mailbox相当于是一个小型的队列，一旦Sender发送消息，就是将该消息入队到Mailbox中。入队的顺序按照消息发送的时间顺序。
<img src="https://github.com/chenjian8541/Actor.Demo/blob/master/Resource/8888.png?raw=true" alt="8888.png">

# Orleans
Orleans是微软开源的分布式框架，提供了一个简单的方法来构建大规模、高并发、分布式应用程序，被认为是Actor模型的分布式版本，是一种改进的Actor模型。在Orleans中，Actors被称作Grains，采用接口来表示，Actors的消息用异步方法来接收。

# Actor.Demo
此demo是模拟并发情况下扣减库存的场景，使用Orleans和gRPC两种不同的rpc框架进行客户端与服务端的通信，Orleans是Actor模型的分布式版本，同一个Silo同一时刻只有一个线程执行，所以能保证库存不会多扣。
</br></br>
使用gRPC运行效果
<img src="https://github.com/chenjian8541/Actor.Demo/blob/master/Resource/111.png?raw=true" alt="111.png">
</br></br>
使用Orleans运行效果
<img src="https://github.com/chenjian8541/Actor.Demo/blob/master/Resource/222.png?raw=true" alt="222.png">
