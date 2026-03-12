using AutoMapper;
using DigitalBankingAPI.Core.Application.Dtos;
using DigitalBankingAPI.Core.Models;

namespace DigitalBankingAPI.Core.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDetails, AccountDetailsDto>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<Transfer, TransferDto>();
            CreateMap<InterestHistory, InterestHistoryDto>();
        }
    }

}
