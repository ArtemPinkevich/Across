namespace UseCases.Handlers.Records.Queries
{
    using MediatR;
    using System.Collections.Generic;
    using UseCases.Handlers.Records.Dto;

    public class GetRecordsByPhoneQuery : IRequest<List<RecordDto>>
    {
        public string PhoneNumber { set; get; }

        public GetRecordsByPhoneQuery(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
