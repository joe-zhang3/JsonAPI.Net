using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using JsonAPI.Net.WebAPI.Resource;
using JsonAPI.Net;

namespace FlexibleJsonAPI.WebAPI.Controllers
{
    [RoutePrefix("test")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("account/{id:int}")]
        public Account GetAccount(int id){

            Account a = new Account() { 
                AccountId = 1, 
                FirstName="Joe", 
                LastName="Zhang", 
                State = new State(){StateId = 1, Name="US"},
            };

            a.Links.Add(new AccountLink("accounts"){Method ="get", Href = new Uri("/accounts")});

            a.Persons = new List<Person>()
            {
                new Person(){PersonId = 1, Name="Lele"},
                new Person(){PersonId = 2, Name="Lele1"},
            };

            return a;
        }

		[HttpGet]
		[Route("account")]
        public IEnumerable<Account> GetAccounts()
		{
            List<Account> accounts = new List<Account>() { 
                new Account() { AccountId = 1, FirstName = "Joe", LastName = "Zhang", Age ="13"} ,
                new Account() { AccountId = 2, FirstName = "Jenner", LastName = "Wang" },
                new Account() { AccountId = 3, FirstName = "Joe", LastName = "Zhang" }
            };

            return accounts;
        }

		[HttpGet]
		[Route("accounts")]
		public JaDocument GetAccounts1()
		{
			List<Account> accounts = new List<Account>() {
				new Account() { AccountId = 1, FirstName = "Joe", LastName = "Zhang"} ,
				new Account() { AccountId = 2, FirstName = "Jenner", LastName = "Wang" },
				new Account() { AccountId = 3, FirstName = "Joe", LastName = "Zhang" }
			};

            JaDocument jd = new JaDocument(accounts);

            jd.Meta.Add("total-counts", accounts.Count);

			return jd;
		}
    }
}
