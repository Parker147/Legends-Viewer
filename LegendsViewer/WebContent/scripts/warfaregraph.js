document.addEventListener('DOMContentLoaded', function () {

    var cy = cytoscape({
        container: document.getElementById('warfaregraph'),

        boxSelectionEnabled: false,
        autounselectify: true,
        zoomingEnabled: true,
        userZoomingEnabled: true,
        wheelSensitivity: 0.1,
        zoom: 1,
        maxZoom: 1,
        minZoom: 0.2,

        style: cytoscape.stylesheet()
            .selector('node')
            .css({
                'shape': 'rectangle',
                'width': 120,
                'height': 'label',
                'content': 'data(name)',
                'background-color': 'data(faveColor)',
                'text-wrap': 'wrap',
                'font-family': 'Helvetica',
                'font-size': 12,
                'text-valign': 'center',
                'text-max-width': 100,
                'padding-top': 5,
                'padding-bottom': 5,
                'color': '#ffffff',
                'text-outline-color': '#000',
                'text-outline-opacity': 0.2,
                'text-outline-width': 2,
                'shadow-blur': 5,
                'shadow-color': '#888888',
                'shadow-offset-x': 3,
                'shadow-offset-y': 3,
                'shadow-opacity': 0.8
            })
            .selector('node.civilization')
            .css({
                'shape': 'octagon',
                'background-image': 'data(icon)',
                'background-clip': 'none',
                'background-width': '24px',
                'background-height': '24px',
                'background-position-x': '-20px',
                'padding-left': 25,
                'padding-right': 25,
                'padding-top': 15,
                'padding-bottom': 15,
                'text-weight': 'bold'
            })
            .selector('node.current')
            .css({
                'background-blacken': 0.15,
                'border-style': 'solid',
                'border-width': 2,
                'border-color': '#000',
                'border-opacity': 0.2,
            })
            .selector('edge')
            .css({
                'label': 'data(label)',
                'color': '#fff',
                'text-outline-width': 2,
                'text-outline-color': 'data(faveColor)',
                'edge-text-rotation': 'autorotate',
                'width': 'data(width)',
                'target-arrow-shape': 'triangle',
                'source-arrow-shape': 'circle',
                'curve-style': 'bezier',
                'line-color': 'data(faveColor)',
                'source-arrow-color': 'data(faveColor)',
                'target-arrow-color': 'data(faveColor)'
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
            idealEdgeLength: function (edge) { return 100; },
            animate: false,
            //fit: false
        }
    });

    cy.on('tap', 'node', function () {
        window.location.href = this.data('href');
    });
    //cy.on('layoutready ', function () {
    //    cy.center();
    //});
});
