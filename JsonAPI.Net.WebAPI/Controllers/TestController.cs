﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http;
using JsonAPI.Net.WebAPI.Resource;

using Newtonsoft.Json.Linq;

namespace JsonAPI.Net.WebAPI
{
    [RoutePrefix("test")]
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("account/{id:int}")]
        public Account GetAccount(int id){
            return buildAccount();
        }

        [HttpPost]
        [Route("account")]
        public State CreateAccount(Account account)
		{
            Account a = account;

            a.State.Name = "accepted";

            return a.State;
		}
		[HttpPost]
		[Route("states")]
		public State CreateState(List<State> states)
		{
            foreach(var state in states)
            {
                
            }
			return null;
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
        [JaResourceTemplate("AccountSearch")]
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
                LastName = "Zhang",
                Complex = new MyComplexObject(),
                Date = DateTime.Now,
                State = new State(){StateId = 1, Address="China", Name="MyState"}
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
