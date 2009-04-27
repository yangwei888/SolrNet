﻿#region license
// Copyright (c) 2007-2009 Mauricio Scheffer
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using MbUnit.Framework;

namespace SolrNet.Tests {
    [TestFixture]
    public class SolrNotQueryTests {
        [Test]
        public void SimpleQuery() {
            var q = new SolrQuery("desc:samsung");
            var notq = new SolrNotQuery(q);
            Assert.AreEqual("-desc:samsung", notq.Query);
        }

        [Test]
        public void QueryByField() {
            var q = new SolrQueryByField("desc", "samsung");
            var notq = new SolrNotQuery(q);
            Assert.AreEqual("-desc:samsung", notq.Query);
        }

        [Test]
        public void RangeQuery() {
            var q = new SolrQueryByRange<decimal>("price", 100, 200);
            var notq = new SolrNotQuery(q);
            Assert.AreEqual("-price:[100 TO 200]", notq.Query);
        }

        [Test]
        public void QueryInList() {
            var q = new SolrQueryInList("desc", "samsung", "hitachi", "fujitsu");
            var notq = new SolrNotQuery(q);
            Assert.AreEqual("-(desc:samsung OR desc:hitachi OR desc:fujitsu)", notq.Query);
        }

        [Test]
        public void MultipleCriteria() {
            var q = SolrMultipleCriteriaQuery.Create(new SolrQueryByField("desc", "samsung"), new SolrQueryByRange<decimal>("price", 100, 200));
            var notq = new SolrNotQuery(q);
            Assert.AreEqual("-(desc:samsung  price:[100 TO 200])", notq.Query);
        }

        [Test]
        public void MultipleCriteria_not() {
            var q = SolrMultipleCriteriaQuery.Create(new SolrQueryByField("desc", "samsung"), new SolrQueryByRange<decimal>("price", 100, 200));
            Assert.AreEqual("-(desc:samsung  price:[100 TO 200])", q.Not().Query);
        }
    }
}