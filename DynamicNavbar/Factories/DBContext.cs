using DynamicNavbar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicNavbar.Factories
{
    public class DBContext
    {
        private AutoFactory<Category> categoryFactory;
        private AutoFactory<FrontPage> frontpageFactory;
        private AutoFactory<Product> productFactory;
        private AutoFactory<Activity> activityFactory;
        private AutoFactory<Article> articleFactory;
        private AutoFactory<Member> memberFactory;

        public AutoFactory<Category> CategoryFactory
        {
            get
            {
                if (categoryFactory == null)
                {
                    categoryFactory = new AutoFactory<Category>();
                }
                return categoryFactory;
            }
        }
        public AutoFactory<FrontPage> FrontpageFactory
        {
            get
            {
                if (frontpageFactory == null)
                {
                    frontpageFactory = new AutoFactory<FrontPage>();
                }
                return frontpageFactory;
            }
        }
        public AutoFactory<Product> ProductFactory
        {
            get
            {
                if (productFactory == null)
                {
                    productFactory = new AutoFactory<Product>();
                }
                return productFactory;
            }
        }
        public AutoFactory<Activity> ActivityFactory
        {
            get
            {
                if (activityFactory == null)
                {
                    activityFactory = new AutoFactory<Activity>();
                }
                return activityFactory;
            }
        }
        public AutoFactory<Article> ArticleFactory
        {
            get
            {
                if (articleFactory == null)
                {
                    articleFactory = new AutoFactory<Article>();
                }
                return articleFactory;
            }
        }
        public AutoFactory<Member> MemberFactory
        {
            get
            {
                if (memberFactory == null)
                {
                    memberFactory = new AutoFactory<Member>();
                }
                return memberFactory;
            }
        }
    }
}