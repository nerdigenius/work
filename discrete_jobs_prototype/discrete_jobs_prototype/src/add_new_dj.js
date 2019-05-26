import React, { Component } from "react";
import "./add_new_dj.css";
import MultipleDj from "./multiple_dj_entry"
import Timer from "./timer";
import TimerStopEntries from "./on_timer_stop_djentries";


class Newdj extends Component {
    constructor(props) {
        super(props)

        this.state = {
            items: [],
            viewList: "hide",
            ProductLine:"Product1",
            DjType:"Type 1",
            Designer:"Designer 1",
            timerStart:false,
            timeElapsed:0
        }
    }

    setTimeElapsed(time){
        this.setState({
            timeElapsed: time
        }, () => {
            console.log(this.state.timeElapsed);
        })
    }

    updateTimerStart(timerState)
    {
        this.setState({
            timerStart: timerState
        }, () => {
            console.log(this.state.timerStart);
        })
    }

    updateitems(itemsInput) {

        this.setState({
            items: itemsInput
        }, () => {
                console.log(this.state.items);
            })
    }

    updateViewList(viewListState) {

        this.setState({
            viewList: viewListState
        }, () => {
            console.log(this.state.viewList);
        })
    }

    onSelectProductLine(e) {
        console.log()

        this.setState({
            ProductLine: e.target.value
        }, () => {
            console.log(this.state.ProductLine);
        })

    }

    onSelectDjType(e) {
        console.log(e.target.value)

        this.setState({
            DjType: e.target.value
        }, () => {
            console.log(this.state.DjType);
        })

    }

    onSelectDesigner(e) {

        this.setState({
            Designer: e.target.value
        }, () => {
            console.log(this.state.Designer);
        })

    }
    render() {
        return (
            <div className='background' >

                Product Line:
                    <select onChange={this.onSelectProductLine.bind(this)} className='dropdown'>
                    <option value="Product1">Product 1</option>
                    <option value="Product2">Product 2</option>
                    <option value="Product3">Product 3</option>
                    <option value="Product4">Product 4</option>
                </select>
                <br /><br />
                Input DJ:<MultipleDj items={this.state.items} updateitems={this.updateitems.bind(this)} />

                <br /><br />
                DJ Type:

                    <select onLoad={this.onSelectDjType.bind(this)} onChange={this.onSelectDjType.bind(this)} className='dropdown'>
                    <option value="Type 1">Type 1</option>
                    <option value="Type 2">Type 2</option>
                    <option value="Type 3">Type 3</option>
                    <option value="Type 4">Type 4</option>
                </select>

                <br /><br />
                Designer:
                    <select onChange={this.onSelectDesigner.bind(this)} className='dropdown'>
                    <option value="Designer 1">Designer 1</option>
                    <option value="Designer 2">Designer 2</option>
                    <option value="Designer 3">Designer 3</option>
                    <option value="Designer 4">Designer 4</option>
                </select>
                <br /><br />
                Design process:<br /><br />
                <div>
                    <Timer viewList={this.state.viewList} updateViewList={this.updateViewList.bind(this)} updateTimerStart={this.updateTimerStart.bind(this)} timeElapsed={this.setTimeElapsed.bind(this)}/>
                </div>

                <br/>

                <div className={this.state.viewList}>
                    <TimerStopEntries entries={this.state.items} viewList={this.state.viewList} DjType={this.state.DjType}  ProductLine={this.state.ProductLine} Designer={this.state.Designer} timeElapsed={this.state.timeElapsed}/> 
                </div>

            </div>
        );
    }
}

export default Newdj;
