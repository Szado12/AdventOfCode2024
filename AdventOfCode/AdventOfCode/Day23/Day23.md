# [Day 23](https://adventofcode.com/2024/day/23)

## Input

The input consists of a list of connections between computers. 
Each line represents a direct connection between two computers, indicated by two computer names separated by a hyphen (`-`).
The connections are bidirectional, meaning `a-b` is the same as `b-a`.

Example input:

```
kh-tc
qp-kh
de-cg
ka-co
yn-aq
qp-ub
cg-tb
vc-aq
tb-ka
wh-tc
yn-cg
kh-ub
ta-co
de-co
tc-td
tb-wq
wh-td
ta-ka
td-qp
aq-cg
wq-ub
ub-vc
de-ta
wq-aq
wq-vc
wh-yn
ka-de
kh-ta
co-tc
wh-qp
tb-vc
td-yn
```

## Part 1

### Problem

The goal is to find sets of three computers that are all directly connected to each other (a LAN party).
Each computer in the set must be connected to the other two computers.

Additionally, only sets containing at least one computer whose name starts with the letter `t` should be counted.

### Solution

To solve this, the input is read and stored as a graph where each computer is a node, and the connections are edges.
The algorithm iterates through each computer whose name starts with `t`, and checks if any two of its neighbors are also connected to each other, forming a clique of three computers.

Example code:

```csharp
private void FindLans(string startComputer)
{
    var tNeighbours = _computers[startComputer];

    foreach (var tNeighbour in tNeighbours)
    {
       var commonNeighbours = tNeighbours.Intersect(_computers[tNeighbour]);
       foreach (var commonNeighbour in commonNeighbours)
       {
           var lanName = new List<string> {commonNeighbour, tNeighbour, startComputer}.Order().ToArray();
           _lans.Add((lanName[0], lanName[1], lanName[2]));
       }
    }
}
```

The final count of such sets is the solution.

## Part 2

### Problem

In this part, the goal is to find the largest group of computers where every computer is directly connected to every other computer in the group.

The answer should be the alphabetically sorted list of computer names, joined by commas.

### Solution

The solution uses the **Bron-Kerbosch algorithm**, which is a recursive backtracking algorithm to find all maximal cliques in a graph.
The largest clique is selected as the result.

Example code:

```csharp
private void BronKerbosch(List<string> currentClique, List<string> candidates, List<string> exclusionSet)
{
    if (candidates.Count == 0 && exclusionSet.Count == 0)
    {
        if (currentClique.Count > biggestClique.Count)
            biggestClique = [..currentClique];
    }

    foreach (var candidate in new List<string>(candidates))
    {
        BronKerbosch([..currentClique, candidate], candidates.Intersect(_computers[candidate]).ToList(), exclusionSet.Intersect(_computers[candidate]).ToList());
        candidates.Remove(candidate);
        exclusionSet.Add(candidate);
    }
}
```

The largest clique is then sorted alphabetically and returned as the password to enter the LAN party.