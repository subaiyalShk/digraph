using System;
using System.Collections.Generic;

namespace DiGraph
{
    public class SimpleDirectedGraph<TV, TK> : AbstractGraph<TV, TK>
    {
        public override bool AddEdge(TV v1, TV v2, TK weight)
        {
            if (v1 == null || v2 == null || weight == null)
                throw new ArgumentNullException();
            if (!VertexSet.Contains(v1) || !VertexSet.Contains(v2))
                return false;
            IPairValue<TV> pair = new PairValue<TV>(v1, v2);
            if (EdgeSet.Contains(pair))
                return false;
            EdgeSet.Add(pair);
            Weights[pair] = weight;
            return true;
        }

        public override TK GetWeight(TV v1, TV v2)
        {
            if (v1 == null || v2 == null)
                throw new ArgumentNullException();
            IPairValue<TV> pair = new PairValue<TV>(v1, v2);
            if (!Weights.ContainsKey(pair))
                throw new ArgumentException();
            return Weights[pair];
        }

        public override bool DeleteEdge(TV v1, TV v2)
        {
            if (v1 == null || v2 == null)
                throw new ArgumentNullException();
            IPairValue<TV> pair = new PairValue<TV>(v1, v2);
            if (EdgeSet.Contains(pair))
            {
                EdgeSet.Remove(pair);
                Weights.Remove(pair);
                return true;
            }
            return false;
        }

        public override bool AreAdjacent(TV v1, TV v2)
        {
            if (v1 == null || v2 == null)
                throw new ArgumentNullException();
            if (!VertexSet.Contains(v1) || !VertexSet.Contains(v2))
                throw new ArgumentException();
            return EdgeSet.Contains(new PairValue<TV>(v1, v2));
        }

        public override int Degree(TV vertex)
        {
            if (vertex == null)
                throw new ArgumentNullException();
            if (!VertexSet.Contains(vertex))
                throw new ArgumentException();
            return InDegree(vertex) + OutDegree(vertex);
        }

        public override int OutDegree(TV vertex)
        {
            if (vertex == null)
                throw new ArgumentNullException();
            if (!VertexSet.Contains(vertex))
                throw new ArgumentException();
            int counter = 0;
            foreach (var pair in EdgeSet)
                if (pair.GetFirst().Equals(vertex))
                    counter++;
            return counter;
        }

        public override int InDegree(TV vertex)
        {
            if (vertex == null)
                throw new ArgumentNullException();
            if (!VertexSet.Contains(vertex))
                throw new ArgumentException();
            int counter = 0;
            foreach (var pair in EdgeSet)
                if (pair.GetSecond().Equals(vertex))
                    counter++;
            return counter;
        }

        public override IEnumerable<TV> AdjacentVertex(TV vertex)
        {
            foreach (IPairValue<TV> p in EdgeSet)
                if (p.GetFirst().Equals(vertex))
                    yield return p.GetSecond();
        }
    }
}