import React, { Component } from 'react';
import { Spring } from 'react-spring/renderprops';

class Grid2 extends Component {
    showMore() {
        let list = [];
        for (var i = 0; i < 10; i++) {

            list.push(<div>
                <img className="gridImage" src={require('../../static/Icon/grid1.png')} />
            </div>)

        }
        return list;
    }
    render() {

        var lisitems = this.showMore();
        return (
            <Spring
                from={{ opacity: 0 }}
                to={{ opacity: 1 }}
                config={{delay:100,duration:1000}}>
                {props => (
                    <div style={props}>
                        <div className="gridContainer2" >
                            {lisitems}
                        </div>
                    </div>
                )}

            </Spring>


        )
    }

}
export default Grid2;