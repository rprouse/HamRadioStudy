﻿using HamRadioStudy.Core.Interfaces;
using HamRadioStudy.Core.Services;

namespace HamRadioStudy;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services.AddTransient<QuestionService>()
                .AddSingleton<IStudyDatabase, StudyDatabase>()
                .AddSingleton<MainPage>();
}
