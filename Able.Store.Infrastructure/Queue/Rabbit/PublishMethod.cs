namespace Able.Store.Infrastructure.Queue.Rabbit
{

    [System.Flags]
    public enum PublishMethod:int
    {
        简单工作队列 = 1,

        发布订阅 =2,

        路由模式=3,

        主题=4,

        请求回复=5,

        发布者确认=6
    }
    [System.Flags]
    public enum  ChangeType:int
    {
        Direct=1,
        Topic=2,
        Fanout =3
    }
}
