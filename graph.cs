using System;
using System.Collections.Generic;
using System.IO;

namespace DiGraph
{
    class Graph
    {
        class Node
        {
            public int index;
            public char data;
            public Node(char data, int index)
            {
                this.index = index;
                this.data = data;
            }
        }

        // class Edge {
        //     private Node start;
        //     private Node end;
        //     private int weight;
        //     public Edge(Node start, Node end, int weight)
        //     {
        //         this.start=start;
        //         this.end=end;
        //         this.weight=weight;
        //     }
        // }
        /// <summary>
        /// 4 attributes
        /// A list of nodes (to store node information for each index such as name/text)
        /// a 2D array - our adjacency matrix, stores edges between nodes
        /// a graphSize integer
        /// a StreamReader, to read in graph data to create the data structure
        /// </summary>
        private List<Node> nodes;
        // private List<Edge> edges;
        private int graphSize;

        // private StreamReader sr;
        private int[,] adjMatrix;
        private const int infinity = 9999;
        public Graph(char [] nodes_input, string [] edges_input)
        {
            // Generating a list of nodes V= {V1, V2, V3,..}
            nodes = new List<Node>();
            for (int i=0; i<nodes_input.Length; i++)
            {
                nodes.Add(new Node(nodes_input[i], i));
            }
            CreateGraph();

            // Generating a list of Edges E= {E1, E2, E3,..}
            for (int i=0; i<edges_input.Length; i++ )
            {
                // parsing input
                char startstr = edges_input[i][0];
                char endstr = edges_input[i][1];
                
                // edges.Add(new Edge(start,end,weight));
                AddEdge(
                    nodes.Find(v => v.data == startstr).index, 
                    nodes.Find(v => v.data == endstr).index,
                    (int)Char.GetNumericValue(edges_input[i][2])
                );
            }
            Display();
        }
        private void CreateGraph()
        {
            //get the graph size first
            graphSize = nodes.Count;//non-zero arrays, add 1
            adjMatrix = new int[graphSize, graphSize];
            
            //ASSUME ALL DATA HAS BEEN READ FROM A TEXT FILE & ADJACENCY MATRIX HAS BEEN INITIALIZED
        }
        public void AddEdge(int NodeA, int NodeB, int distance)
        {
            // if (NodeA > 0 && NodeB > 0 && NodeA <= graphSize && NodeB <= graphSize)
            // {
                adjMatrix[NodeA, NodeB] = distance;
            // }
        }
        public void RemoveEdge(int NodeA, int NodeB)
        {
            if (NodeA > 0 && NodeB > 0 && NodeA <= graphSize && NodeB <= graphSize)
            {
                adjMatrix[NodeA, NodeB] = 0;
            }
        }
        public bool Adjacent(int NodeA, int NodeB)
        {   //checks whether two nodes are adjacent, returns true or false
            return (adjMatrix[NodeA, NodeB] > 0);
        }

        public int GetNodeIndex(char node_data){
            int index = nodes.Find(node => node.data == node_data).index;
            return index;
        }

        public int GetEdgeWeight(int start, int end){
            return adjMatrix[start,end];
        }
        public List<int> ListNeighbours(int vertex)
        {
            /* create a new list */
            List<int> neighbours= new List<int>();
            
            for(int j=0; j<graphSize; j++){
            /* if j is a neighbour of i then add it to the list */
                if(adjMatrix[vertex,j]>0){
                    neighbours.Add(adjMatrix[vertex,j]);
                }
            }
            /* return the list */
            return neighbours;
        }
        
        public void Display() //displays the adjacency matrix
        {
            Console.WriteLine("***********Adjacency Matrix Representation***********");
            Console.WriteLine("Number of nodes: {0}\n", graphSize);
            Console.Write("\t");
            foreach (Node n in nodes)
            {
                Console.Write("{0}\t", n.data);
            }
            Console.WriteLine();
            Console.WriteLine();//newline for the graph display
            for (int i = 0; i < graphSize; i++)
            {
                Console.Write("{0}\t", nodes[i].data);
                for (int j = 0; j < graphSize; j++)
                {
                    Console.Write("{0}\t", adjMatrix[i,j]);
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine("Read the graph from left to right");
        }
        private void DisplayNode(int v)//displays data/description for a node
        {
            Console.WriteLine(nodes[v].data);
        }
    }

}