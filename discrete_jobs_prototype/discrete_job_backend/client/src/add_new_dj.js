import React, { Component } from "react";
import "./add_new_dj.css";
import MultipleDj from "./multiple_dj_entry"
import Timer from "./timer";
import TimerStopEntries from "./on_timer_stop_djentries";
import axios from 'axios';



class Newdj extends Component {
    constructor(props) {
        super(props)

        this.state = {
            items: [],
            viewList: "hide",
            ProductLine:"PFL",
            DjType:"VIPS",
            Designer:"Designer 1",
            timerStart:false,
            timeElapsed:0,
            InputDisabled:true,
            apiData:""
        }
        const url = `https://ad-prototype.herokuapp.com/api`;
        // fetch(url)
        // .then(response => response.json())
        // .then(data => this.setState({ apiData:data },()=>{console.log(this.state.apiData);}))
        // .then(()=>{this.setInputDisabled(false)})
        // .catch(e => console.log('error', e));
        axios.get(url).then(res => {
            console.log(res.data);
            this.setState({apiData: res.data}, () => {
                this.setInputDisabled(false);
            })
            
        })
        
    }

    setInputDisabled(condition){
        this.setState({
            InputDisabled: condition
        }, () => {
            console.log(this.state.InputDisabled);
        })
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
                    <option value="PFL">PFL</option>
                    <option value="HTL">HTL</option>
                    <option value="Offset">Offset</option>
                    <option value="Digital">Digital</option>
                </select>
                <br /><br />
                Input DJ:<MultipleDj items={this.state.items} updateitems={this.updateitems.bind(this)} InputDisabled ={this.state.InputDisabled}/>

                <br /><br />
                Design process:

                    <select onLoad={this.onSelectDjType.bind(this)} onChange={this.onSelectDjType.bind(this)} className='dropdown'>
                    <option value="VIPS">VIPS</option>
                    <option value="Manual common">Manual common</option>
                    <option value="Manual variable">Manual variable</option>
                    <option value="PAS">PAS</option>
                    <option value="GPM">GPM</option>
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
                <br /><br />
                <div>
                    <Timer viewList={this.state.viewList} updateViewList={this.updateViewList.bind(this)} updateTimerStart={this.updateTimerStart.bind(this)} timeElapsed={this.setTimeElapsed.bind(this)}/>
                </div>

                <br/>

                <div className={this.state.viewList}>
                    <TimerStopEntries entries={this.state.items} viewList={this.state.viewList} DjType={this.state.DjType}  ProductLine={this.state.ProductLine} Designer={this.state.Designer} timeElapsed={this.state.timeElapsed} InputDisbaled={this.setInputDisabled.bind(this)} apiData={this.state.apiData}/> 
                </div>

            </div>
        );
    }
}

export default Newdj;
