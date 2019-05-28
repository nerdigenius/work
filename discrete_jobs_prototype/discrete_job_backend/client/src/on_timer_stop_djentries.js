import React, { Component } from "react"
import axios from 'axios';


class TimerStopEntries extends Component {

    constructor(props){
        super(props)

        this.state={
            apiData:""
        }
        // const url = `http://localhost:5000/api`;
        // // fetch(url)
        // // .then(response => response.json())
        // // .then(data => this.setState({ apiData:data },()=>{console.log(this.state.apiData);}))
        // // .catch(e => console.log('error', e));
        // axios.get(url).then(res => {
        //     console.log(res.data);
        //     this.setState({apiData: res.data})
        // })
    }

    createTasks(items) {
        var json=this.props.apiData
        // console.log(items);
        let pickone=[]
        
        console.log(json);
        
        json.forEach(element => {

            
            
            if(items.text === element.DiscreteJob){
                pickone.push(element)   
            }
            
        });
 
        console.log("iasdnjafdknj",pickone)
        let Quantity = pickone[0] !== undefined ? pickone[0].Quantity : 'N/A';
        let RPO = pickone[0] !== undefined ? pickone[0].RBOName : 'N/A';

        return (
        // <li key={items.key}>{items.text} </li>
        
        <tbody key={items.key}>
            <tr >
                <td>{items.text}</td>
                <td>{this.props.ProductLine}</td>
                <td>{this.props.DjType}</td>
                <td>{this.props.Designer}</td>
                <td>{this.props.timeElapsed}</td>
                <td>{Quantity}</td>
                <td>{RPO}</td>
            </tr>
        </tbody>
        );
        
    }

    render() {
        var todoEntries = this.props.entries;
        
       // console.log(todoEntries)
        var listItems = todoEntries.map(this.createTasks.bind(this));

        return (
            // <ul className="thelist2">
            //     <h1>kasfknadflk</h1>
            //     {listItems}
            // </ul>
            <table id="customers">
                <tbody>
                <tr>
                    <th>Dj Number</th>
                    <th>Product Line</th>
                    <th>DJ Type</th>
                    <th>Designer</th>
                    <th>TIme Elapsed</th>
                    <th>Quantity</th>
                    <th>RBO</th>
                </tr>
                </tbody>
                {listItems}


            </table>
        )
    }

}

export default TimerStopEntries; 