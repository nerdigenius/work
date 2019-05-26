import React, { Component } from "react";
import DjEntries from "./dj_entries";
class MultipleDj extends Component {

    constructor(props) {
        super(props);

        this.state = {
            items: []
        };

        this.addItem = this.addItem.bind(this);
    }

    addItem(e) {
        

        let itemSt = this.state.items;
        if (this._inputElement.value !== "") {
            console.log('if state')
            var newItem = {
                text: this._inputElement.value,
                key: Date.now()
            };
            itemSt.push(newItem);

            this.setState({
                items: itemSt
            });
            // console.log(this.props);
            
             this.props.updateitems(this.state.items)
        }
        e.preventDefault();


        this._inputElement.value = "";
        
    }

    render() {
        return (
            <div className="todoListMain">

                <form onSubmit={this.addItem}>
                    <input ref={(a) => this._inputElement = a}
                        placeholder="Enter Task" />
                    <button type="submit">add</button>
                </form>

                <DjEntries entries={this.state.items} />
            </div>
        );
    }
}

export default MultipleDj;