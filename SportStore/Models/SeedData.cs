using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            StoreDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Фляжка Stern",
                        Category = "Шейкеры и бутылки",
                        Description = "Спортивная фляжка для протеина/воды",
                        Price = 279,
                        ImageFilePath = "Фляжка Stern"
                    },

                    new Product
                    {
                        Name = "Фляжка Stern CBOT-4",
                        Category = "Шейкеры и бутылки",
                        Description = "Тритановая фляга Stern объемом 750 мл пригодится на велопрогулке.",
                        Price = 1099,
                        Discount = 10,
                        ImageFilePath = "Фляжка Stern CBOT-4"
                    },

                    new Product
                    {
                        Name = "Фляжка Stern Vicky",
                        Category = "Шейкеры и бутылки",
                        Description = "Детская фляжка Stern объемом 350 мл. С помощью удобного держателя фляжку можно закрепить на руле велосипеда.",
                        Price = 237,
                        ImageFilePath = "Фляжка Stern Vicky"
                    },

                    new Product
                    {
                        Name = "Бутылка для воды Nike Accessories",
                        Category = "Шейкеры и бутылки",
                        Description = "Спортивная бутылка для воды",
                        Price = 1189,
                        ImageFilePath = "Бутылка для воды Nike Accessories"
                    },

                     new Product
                     {
                         Name = "Бутылка для воды Kettler 0,5 л",
                         Category = "Шейкеры и бутылки",
                         Description = "Бутылка для воды от Kettler объемом 0,5 л станет отличным выбором для тренировок. Модель, изготовленная из долговечного и прочного экозена, не впитывает запахи. Снабжена надежной системой фиксации. Изделие можно мыть в посудомоечной машине",
                         Price = 1099,
                         ImageFilePath = "Бутылка для воды Kettler 0,5 л"
                     },

                     new Product
                     {
                         Name = "Бутылка для воды Demix, 0,8 л",
                         Category = "Шейкеры и бутылки",
                         Description = "Бутылка для воды объемом 800 мл, изготовленная из тритана. Не содержит бисфенол-А. Благодаря силиконовой вставке бутылку удобно держать в руке. Модель можно мыть в посудомоечной машине.",
                         Price = 1199,
                         ImageFilePath = "Бутылка для воды Demix, 0,8 л"
                     },


                    new Product
                    {
                        Name = "Маска Uvex Athletic LGL",
                        Category = "Маски",
                        Description = "Маска Uvex Athletic разработана совместно со спортсменами. Угловой дизайн оправы и широкая линза гарантируют максимальный обзор при катании на скоростных трассах или в стиле бэккантри.",
                        Price = 5599,
                        ImageFilePath = "Маска Uvex Athletic LGL"
                    },

                    new Product
                    {
                        Name = "Маска горнолыжная Glissade Pilot",
                        Category = "Маски",
                        Description = "Базовая горнолыжная маска подойдет для катания в облачную погоду или при искусственном освещении.",
                        Price = 1499,
                        Discount = 15,
                        ImageFilePath = "Маска горнолыжная Glissade Pilot"
                    },

                    new Product
                    {
                        Name = "Маска со сменной линзой Anon M2 W/SPR",
                        Category = "Маски",
                        Description = "Маска со сменной линзой Anon M2 W/SPR подойдет для катания в солнечную погоду.",
                        Price = 29999,
                        ImageFilePath = "Маска со сменной линзой Anon M2 WSPR"
                    },

                    new Product
                    {
                        Name = "Маска со сменной линзой Anon M2 W/SPR",
                        Category = "Маски",
                        Description = "Маска со сменной линзой Anon M2 W/SPR подойдет для катания в солнечную погоду.",
                        Price = 29999,
                        ImageFilePath = "Маска со сменной линзой Anon M2 WSPR"
                    },

                    new Product
                    {
                        Name = "Маска Uvex Downhill 2000 FM",
                        Category = "Маски",
                        Description = "Маска Uvex Downhill 2000 с отличным обзором. Световой фильтр S2.",
                        Price = 13999,
                        Discount = 32,
                        ImageFilePath = "Маска со сменной линзой Anon M2 WSPR"
                    },

                    new Product
                    {
                        Name = "Маска детская Uvex Speedy Pro",
                        Category = "Маски",
                        Description = "Маска Uvex, разработанная специально для детей и подростков. Световой фильтр S2.",
                        Price = 2599,
                        ImageFilePath = "Маска детская Uvex Speedy Pro"
                    },

                    new Product
                    {
                        Name = "Маска Dragon D1 Bonus",
                        Category = "Маски",
                        Description = "Универсальная горнолыжная маска Dragon D1 с цилиндрической линзой и широким периферийным обзором. Модель рекомендована для неяркого солнца..",
                        Price = 12899,
                        ImageFilePath = "Маска Dragon D1 Bonus"
                    },


                    new Product
                    {
                        Name = "Мяч баскетбольный Demix",
                        Category = "Мячи",
                        Description = "Баскетбольный мяч Demix 5 размера с классической 8-панельной конструкцией для детских тренировок.",
                        Price = 649,
                        ImageFilePath = "Мяч баскетбольный Demix"
                    },

                    new Product
                    {
                        Name = "Мяч для пляжного волейбола Wilson Shoreline",
                        Category = "Мячи",
                        Description = "Волейбольный мяч Wilson Shoreline. Яркий дизайн позволяет лучше контролировать полет мяча.",
                        Price = 1899,
                        Discount = 45,
                        ImageFilePath = "Мяч для пляжного волейбола Wilson Shoreline"
                    },

                    new Product
                    {
                        Name = "Мяч волейбольный Demix",
                        Category = "Мячи",
                        Description = "Любительский волейбольный мяч Demix.",
                        Price = 649,
                        ImageFilePath = "Мяч волейбольный Demix"
                    },

                     new Product
                     {
                         Name = "Мяч футбольный Demix",
                         Category = "Мячи",
                         Description = "Футбольный мяч из профессиональной серии Demix подходит для проведения международных соревнований.",
                         Price = 2299,
                         ImageFilePath = "Мяч футбольный Demix"
                     },

                     new Product
                     {
                         Name = "Кроссовки мужские PUMA X-Ray 2 Square",
                         Category = "Обувь",
                         Description = "Кроссовки X-Ray² Square, выполненные по мотивам моделей \"нулевых\", помогут создать стильный современный образ.",
                         Price = 8999,
                         Discount = 20,
                         ImageFilePath = "Кроссовки мужские PUMA X-Ray 2 Square"
                     },

                     new Product
                     {
                         Name = "Кроссовки мужские Demix Sprinter Vibe",
                         Category = "Обувь",
                         Description = "Кроссовки Demix Sprinter Vibe сочетают в себе вдохновение ретро-моды и современного урбанистического стиля.",
                         Price = 5199,
                         ImageFilePath = "Кроссовки мужские Demix Sprinter Vibe"
                     },

                     new Product
                     {
                         Name = "Кроссовки мужские PUMA RS-Z Core",
                         Category = "Обувь",
                         Description = "Кроссовки RS-Z Core для тех, кто стремится создать актуальный и комфортный образ.",
                         Price = 12999,
                         ImageFilePath = "Кроссовки мужские PUMA RS-Z Core"
                     },

                     new Product
                     {
                         Name = "Кроссовки мужские FILA Trace Low",
                         Category = "Обувь",
                         Description = "Кроссовки FILA Trace Low помогут создать запоминающийся образ в спортивном стиле",
                         Price = 8499,
                         ImageFilePath = "Кроссовки мужские FILA Trace Low"
                     },

                     new Product
                     {
                         Name = "Кроссовки мужские FILA Proton",
                         Category = "Обувь",
                         Description = "Кроссовки FILA Proton станут отличным выбором для тренировок. Светоотражающие элементы сделают вас заметнее в темноте, что важно во время занятий спортом на свежем воздухе.",
                         Price = 7799,
                         ImageFilePath = "Кроссовки мужские FILA Proton"
                     },

                     new Product
                     {
                         Name = "Кроссовки мужские Demix Ucrazy Enrblast M",
                         Category = "Обувь",
                         Description = "Технологичные кроссовки Demix Ucrazy эффектно дополнят ваш спортивный образ.",
                         Price = 7799,
                         ImageFilePath = "Кроссовки мужские Demix Ucrazy Enrblast M"
                     }
                );
                context.SaveChanges();
            }
        }
    }
}
