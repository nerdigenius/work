import React from 'react'
import { Spring } from 'react-spring/renderprops';

export default function Grid1() {
    return (

        <Spring
            from={{ opacity: 0 }}
            to={{ opacity: 1 }}
            config={{delay:100,duration:1000}}>
            {props => (
                <div style={props}>
                    <div className="gridContainer">
                        <div className="gridItem">
                            <img className="gridImage" src={require('../../static/Icon/grid1.png')} />
                        </div>
                        <div className="gridItem">
                            <img className="gridImage" src={require('../../static/Icon/grid2.png')} />
                        </div>
                        <div className="gridItem">
                            <img className="gridImage" src={require('../../static/Icon/grid3.png')} />
                        </div>
                        <div className="gridItem">
                            <span className="text">LIVE WORK REPEAT DIE HATE</span>
                        </div>
                        <div className="gridItem">
                            <span className="text">LIVE WORK REPEAT DIE HATE</span>
                        </div>
                        <div className="gridItem">
                            <img className="gridImage" src={require('../../static/Icon/grid5.png')} />
                        </div>
                        <div className="gridItem">
                            <img className="gridImage" src={require('../../static/Icon/grid6.png')} />
                        </div>
                        <div className="gridItem">
                            <span className="text">LIVE WORK REPEAT DIE HATE</span>
                        </div>
                        <div className="gridItem">
                            <img className="gridImage" src={require('../../static/Icon/grid7.png')} />
                        </div>
                        <div className="gridItem">
                            <img className="gridImage" src={require('../../static/Icon/gridlowImg.png')} />
                        </div>



                    </div>
                </div>
            )}

        </Spring>


    )
}