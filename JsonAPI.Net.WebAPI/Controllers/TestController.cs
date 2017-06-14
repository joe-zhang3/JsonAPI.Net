using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using JsonAPI.Net.WebAPI.Resource;
using JsonAPI.Net;

namespace JsonAPI.Net.WebAPI
{
    [RoutePrefix("test")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("account/{id:int}")]
        [JaAction(masterTemplate:"Master")]
        public Account GetAccount(int id){
            return buildAccount();
        }

		[HttpGet]
		[Route("account")]
        public IEnumerable<Account> GetAccounts()
		{
            List<Account> accounts = new List<Account>() { 
                buildAccount() ,
                buildAccount(),
				buildAccount(),
			};

            return accounts;
        }

		[HttpGet]
		[Route("accounts")]
		public JaDocument GetAccounts1()
		{
    		List<Account> accounts = new List<Account>() {
				buildAccount() ,
				buildAccount(),
				buildAccount(),
			};

            JaDocument jd = new JaDocument(accounts);

            jd.Meta.Add("total-counts", accounts.Count);

            jd.OfLink(new JaSimpleLink("self", new Uri("/abc")));
            jd.OfLink(new JaSimpleLink("related", new Uri("/abc")));
            jd.OfLink(new JaSimpleLink("new", new Uri("/abc")));

			return jd;
		}

        private Account buildAccount(){
			Account a = new Account()
			{
                AccountId = new Random().Next(),
				FirstName = "Joe",
				LastName = "Zhang"
			};

            a.OfLink(new AccountLink("accounts") { Href = new Uri("/accounts"), Method = "get", Test="test" });

			a.Persons = new List<Person>()
			{
				new Person(){PersonId = new Random().Next(), Name="Lele"},
				new Person(){PersonId = new Random().Next(), Name="Lele1"},
			};

			Person p = new Person() { PersonId = new Random().Next(), Name = "Lele2" };

			p.Links.Add(new JaSimpleLink("self", new Uri("/persons")));

            return a;
        }
    }
}
