using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Skclusive.Extensions.DependencyInjection;
using System;
using Skclusive.Text.Json;
using System.Text.Json.Serialization;
using static Skclusive.FlightFinder.App.State.AppTypes;

namespace Skclusive.FlightFinder.App.State
{
    public static class FlightFinderExtension
    {
        public static void TryAddFlightFinderStateServices(this IServiceCollection services)
        {
            services.TryAddJsonTypeConverter<IAirportSnapshot, AirportSnapshot>();
            services.TryAddJsonTypeConverter<ISearchCriteriaSnapshot, SearchCriteriaSnapshot>();
            services.TryAddJsonTypeConverter<IFlightSegmentSnapshot, FlightSegmentSnapshot>();
            services.TryAddJsonTypeConverter<IItinerarySnapshot, ItinerarySnapshot>();
            services.TryAddJsonTypeConverter<IAppStateSnapshot, AppStateSnapshot>();

            services.TryAddJsonTypeConverter<IRootSnapshot, RootSnapshot>();
            services.TryAddJsonTypeConverter<IBranchSnapshot, BranchSnapshot>();
            services.TryAddJsonTypeConverter<ITreeSnapshot, TreeSnapshot>();

            services.TryAddScoped((_) => AppStateType.Create(new AppStateSnapshot
            {
                SearchInProgress = false,

                SortOrder = SortOrder.Price,

                SearchCriteria = new SearchCriteriaSnapshot()
                {
                    FromAirport = "LHR",

                    ToAirport = "SEA",

                    OutboundDate = DateTime.Now.Date,

                    ReturnDate = DateTime.Now.Date.AddDays(7)
                },

                Root = new RootSnapshot
                {
                    Tree = new TreeSnapshot
                    {
                        Branches = new IBranchSnapshot []
                        {
                            new BranchSnapshot { Name = "branch 1" },

                            new BranchSnapshot { Name = "branch 2" }
                        }
                    }
                }
            }));

            services.TryAddScoped<IAppService, AppService>();
        }
    }
}
