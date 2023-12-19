namespace UseCases.Handlers.WashServices.Dto
{
    using System.Collections.Generic;

    public class WashServiceDto
    {
        public int Id { set; get; }
        public bool Enabled { set; get; }
        public string Name { set; get; }
        public List<WashServiceSettingsDto> WashServiceSettingsDtos { set; get; }
        public string Description { set; get; }
        public List<int> Composition { set; get; }
    }
}
