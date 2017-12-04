namespace LendYourHome.Application.Models.HomesViewModels
{
    using Services.ServiceModels.Homes;
    using System.Collections.Generic;

    public class HomesDisplayViewModel
    {
        public HomesSearchingViewModel FormSearch { get; set; }

        public IEnumerable<HomeOfferServiceModel> Homes { get; set; }
    }
}
