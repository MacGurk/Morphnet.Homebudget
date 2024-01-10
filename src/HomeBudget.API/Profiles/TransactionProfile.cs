using AutoMapper;
using HomeBudget.API.Entities;
using HomeBudget.API.Models.Transaction;

namespace HomeBudget.API.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, TransactionDto>().ReverseMap();
        CreateMap<TransactionStatistics, TransactionStatisticsDto>();
        CreateMap<TransactionForCreationDto, Transaction>();
        CreateMap<TransactionForUpdateDto, Transaction>();
    }
}