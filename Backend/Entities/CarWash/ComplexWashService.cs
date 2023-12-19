namespace Entities
{
    using System.Collections.Generic;


    public class ComplexWashService: WashService
    {
        public List<WashService> WashServices { set; get; }
    }
}
