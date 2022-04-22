import sys
import json
from graphviz import Digraph

def show_graph(json_path):
    json_open = open(json_path, 'r')
    nodes = json.load(json_open)

    g = Digraph()
    g.attr('node', shape='rect')
    for node in nodes:
        node_type = node['NodeType']
        if node_type == 'class':
            color = 'green'
        elif node_type == 'interface':
            color = 'blue'
        else:
            color = 'white'
        g.node(node['Name'], color=color)
        for adj in node['Adj']:
            g.edge(node['Name'], adj)

    g.view()

def main():
    args = sys.argv[1:]
    show_graph(args[0])

if __name__ == "__main__":
    main()
