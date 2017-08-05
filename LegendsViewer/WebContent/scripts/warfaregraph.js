document.addEventListener('DOMContentLoaded', function () {

    var cy = cytoscape({
        container: document.getElementById('warfaregraph'),

        boxSelectionEnabled: false,
        autounselectify: true,
        userZoomingEnabled: true,
        wheelSensitivity: 0.1,
        zoom: 1,
        maxZoom: 1,

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
            .selector('node.current')
            .css({
                'border-style': 'dashed',
                'color': '#222299'
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
            nodes: window.warfaregraph_nodes,
            edges: window.warfaregraph_edges
        },

        layout: {
            name: 'cose',
            //fit: false,
            //padding: 30
        }
    });

    cy.center();

    cy.on('tap', 'node', function () {
        window.location.href = this.data('href');
    });
});
