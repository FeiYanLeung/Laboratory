using Laboratory.LinqTest.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Laboratory.LinqTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get { return "Linq"; }
        }


        public class user
        {
            public user(Guid id, string name)
            {
                this.id = id;
                this.name = name;
            }
            public Guid id { get; set; }
            public string name { get; set; }
        }

        public class emp
        {
            public emp(Guid uid, Guid empid)
            {
                this.uid = uid;
                this.empid = empid;
            }
            public Guid uid { get; set; }
            public Guid empid { get; set; }
        }

        public class dept
        {
            public dept(Guid uid, Guid empid, string name)
            {
                this.uid = uid;
                this.empid = empid;
                this.name = name;
            }
            public Guid uid { get; set; }
            public Guid empid { get; set; }
            public string name { get; set; }
        }

        public void Run()
        {
            var uid = Guid.NewGuid();
            var uid2 = Guid.NewGuid();

            var users = new List<user>() {
                new user(uid,"张三"),
                new user(uid2,"李四")
            };

            var empid = Guid.NewGuid();

            var emps = new List<emp>()
            {
                new emp(uid,empid),
                new emp(uid2,Guid.NewGuid())
            };

            var depts = new List<dept>()
            {
                new dept(uid, empid,"部门1"),
                new dept(uid,empid,"部门2")
            };

            var xs = from e in emps
                     join u in users on e.uid equals u.id
                     join d in depts on new { e.uid, e.empid } equals new { d.uid, d.empid } into ld    //多条件
                     select new
                     {
                         uid = u.id,
                         name = u.name,
                         empid = e.empid,
                         depts = ld.Select(q => q.name)
                     };

            foreach (var x in xs)
            {
                Console.WriteLine("uid: {0}；name:{1}；empid:{2}；depts:{3}；", x.uid, x.name, x.empid, string.Join(",", x.depts));
            }


            return;
            linqToObject();
            return;
            var first = new List<int>() { 2, 3 };
            var second = new List<int>() { 3, 4, 5 };

            //差集
            var except_emps = first.Except(second);    //2
            //增加
            var add_emps = second.Except(first);   //4,5

            Console.WriteLine(String.Join(",", except_emps));
            Console.WriteLine(String.Join(",", add_emps));
        }

        private void linqToEntities()
        {
            #region select 和selectMany

            var collection = new List<List<BaseEntity>>()
            {
                new List<BaseEntity>(){
                    new BaseEntity(){  Id= 1 },
                    new BaseEntity(){  Id= 2 }
                }
            };

            Console.WriteLine("Select：");
            Console.WriteLine("如果是包含嵌套对象，则Select不会将符合条件的数据重新整理成一个对象");
            Console.WriteLine("例如：List<List<int>>，在使用Select时返回的对象类型依然为List<List<int>>,当使用SelectMany时返回对象为List<int>类型。");

            collection.Select(q => q).ToList()
                .ForEach((c) =>
                {
                    c.ForEach(e => Console.WriteLine(e.Id));
                });

            Console.WriteLine("SelectMany：");
            collection.SelectMany(q => q).ToList()
                .ForEach((c) =>
                {
                    Console.WriteLine(c.Id);
                });

            #endregion

            #region linq to entities

            var dptMembers = new List<DptMember>();
            for (int i = 1; i <= 5; i++)
            {
                if (i == 3)
                {
                    dptMembers.Add(new DptMember()
                    {
                        DptId = i,
                        DptName = string.Concat("dpt", i),
                        EmpId = Guid.NewGuid()
                    });
                }

                dptMembers.Add(new DptMember()
                {
                    DptId = i,
                    DptName = string.Concat("dpt", i),
                    EmpId = Guid.NewGuid()
                });
            }

            dptMembers.GroupBy(g => g.DptId)
                .Select(q => new
                {
                    dptId = q.Key,
                    q
                })
                .ToList()
                .ForEach(e =>
                {
                    Console.WriteLine("{0}->{1}", e.dptId, e.q.Count());
                });

            #endregion
        }

        private void linqToSql()
        {
            #region linq to sql

            var dtable = new DataTable();
            dtable.Columns.Add("dptid");
            dtable.Columns.Add("dptname");
            dtable.Columns.Add("empid");
            for (int i = 1; i <= 5; i++)
            {
                var drows = dtable.NewRow();
                if (i == 3)
                {
                    var drows_clone = dtable.NewRow();
                    drows_clone["dptid"] = i;
                    drows_clone["dptname"] = string.Concat("dpt", i);
                    drows_clone["empid"] = string.Concat("emp", "_", i);
                    dtable.Rows.Add(drows_clone);
                }
                drows["dptid"] = i;
                drows["dptname"] = string.Concat("dpt", i);
                drows["empid"] = string.Concat("emp", "_", i);
                dtable.Rows.Add(drows);
            }
            var dresult = from p in dtable.AsEnumerable()
                          group p by p.Field<string>("dptname")
                              into g
                          select new
                          {
                              dptname = g.Key,
                              emps = g
                          };

            foreach (var item in dresult)
            {
                if (item.emps.Any())
                {
                    Console.WriteLine("{0}: {1}", item.dptname, item.emps.Count());
                    foreach (var emp in item.emps)
                    {
                        Console.WriteLine("{0}-> {1} {2} {3}", item.dptname, emp[0], emp[1], emp[2]);
                    }
                }
            }

            #endregion
        }

        private void linqToObject()
        {
            #region 组连接(TSQL:group by)

            var group_query = from publisher in SampleData.Publishers
                              join book in SampleData.Books
                              on publisher equals book.Publisher into publisherBooks
                              select new
                              {
                                  PublisherName = publisher.Name,
                                  Books = publisherBooks
                              };

            //使用group
            var query_by_group = from book in SampleData.Books
                                 group book by book.Publisher into grouping
                                 select new
                                 {
                                     PublisherName = grouping.Key.Name,
                                     Books = grouping
                                 };

            #endregion

            #region 内链查询(TSQL：inner join)

            //join查询语句
            var join_query = from publisher in SampleData.Publishers
                             join book in SampleData.Books
                             on publisher equals book.Publisher
                             select new
                             {
                                 PublisherName = publisher.Name,
                                 BookName = book.Title
                             };

            //join操作符语句
            var join_query2 = SampleData.Publishers.Join(
                SampleData.Books,               //join 对象
                publisher => publisher,         //外部的key
                book => book.Publisher,         //内部的key
                (publisher, book) => new        //结果
                {
                    PublisherName = publisher.Name,
                    BookName = book.Title
                });

            #endregion

            #region 左外连接(TSQL：left join)

            //left join, 为空时用default
            var left_join_query_by_default = from publisher in SampleData.Publishers
                                             join book in SampleData.Books
                                             on publisher equals book.Publisher into publisherBooks
                                             from book in publisherBooks.DefaultIfEmpty()
                                             select new
                                             {
                                                 PublisherName = publisher.Name,
                                                 BookName = (book == default(Book)) ? "no book" : book.Title
                                             };

            //left join, 为空时使用默认对象
            var left_join_query = from publisher in SampleData.Publishers
                                  join book in SampleData.Books
                                  on publisher equals book.Publisher into publisherBooks
                                  from book in publisherBooks.DefaultIfEmpty(
                                    new Book { Title = "" }                         //设置为空时的默认值
                                  )
                                  select new
                                  {
                                      PublisherName = publisher.Name,
                                      BookName = book.Title
                                  };

            #endregion

            #region 交叉连接(TSQL：cross join)

            var cross_join_query = from publisher in SampleData.Publishers
                                   from book in SampleData.Books
                                   select new
                                   {
                                       PublisherName = publisher.Name,
                                       BookName = book.Title
                                   };

            //不使用查询表达式
            var cross_join_query2 = SampleData.Publishers.SelectMany(publisher => SampleData.Books.Select(
                book => new
                {
                    PublisherName = publisher.Name,
                    BookName = book.Title
                }));

            #endregion
        }
    }
}
