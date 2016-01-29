document.addEventListener('DOMContentLoaded', function () {
    var cy = cytoscape({
        container: document.getElementById('familygraph'),

        boxSelectionEnabled: false,
        autounselectify: true,
        userZoomingEnabled: true,
        wheelSensitivity: 0.1,
        zoom: 1,


        style: cytoscape.stylesheet()
          .selector('node')
            .css({
                'shape': 'roundrectangle',
                'width': 120,
// ReSharper disable InvalidValue
                'height': 'label',
                'content': 'data(name)',
                'background-color': 'data(faveColor)',
                'text-wrap': 'wrap',
// ReSharper restore InvalidValue
                'font-family': 'Helvetica',
                'font-size': 12,
                'text-valign': 'center',
                'text-max-width': 100,
                'padding-top': 5,
                'padding-bottom': 5,
                'color': '#ffffff',
                'border-width': 1,
                'border-style': 'solid',
                'border-color': '#888888',
                'shadow-blur': 5,
                'shadow-color': '#888888',
                'shadow-offset-x': 3,
                'shadow-offset-y': 3,
                'shadow-opacity': 0.8
            })
          .selector('node.cursed')
            .css({
                'shape': 'rectangle',
                'background-blacken': 0.4,
                'border-style': 'dotted',
                'text-weight': 'bold',
            })
          .selector('node.current')
            .css({
                'background-blacken': -0.6,
                'border-style': 'dashed',
                'color': '#222299',
            })
          .selector('node.dead')
            .css({
                'background-opacity': 0.75,
                'shadow-opacity': 0
            })
          .selector('edge')
            .css({
                'width': 1,
                'target-arrow-shape': 'triangle',
                'curve-style': 'bezier'
            })
          .selector(':selected')
            .css({
                'background-color': 'black',
                'line-color': 'black',
                'target-arrow-color': 'black',
                'source-arrow-color': 'black'
            }),

        elements: {
            nodes: window.familygraph_nodes,
            edges: window.familygraph_edges,
        },

        layout: {
            name: 'breadthfirst',
            fit: false,
            directed: true,
            padding: 10,
            spacingFactor: 1.1,
            avoidOverlap: true,
        }
    });

    cy.on('tap', 'node', function () {
        window.location.href = this.data('href');
    });
});