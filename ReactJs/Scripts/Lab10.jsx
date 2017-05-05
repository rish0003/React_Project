var Address = React.createClass({

    handlerChange: function (e) {
        var addr = this.props.address;

        if (e.target.id == "txtStreet")
            addr.Street = e.target.value;
        else if (e.target.id == "txtCity")
            addr.City = e.target.value;
        if (e.target.id == "drpProvince")
            addr.Province = e.target.value;
        if (e.target.id == "txtPostalCode")
            addr.PostalCode = e.target.value;

        this.props.onAddressChange(addr);
    },
    render: function () {
        return (
          <div>
             <div className="row">
                <div className="col-md-2 label-align">
                      <label htmlFor="txtStreet">Street:</label>
                </div>
                  <div className="col-md-10">
                    <input type="text" id="txtStreet" className="form-control"
                           onChange={this.handlerChange} value={this.props.address.Street} />
                  </div>
             </div>
                      <div className="row">
                         <div className="col-md-2 label-align">
        <label htmlFor="txtCity">City:</label>
                         </div>
                         <div className="col-md-10">
                             <input type="text" id="txtCity" className="form-control"
                                    onChange={this.handlerChange}
                                    value={this.props.address.City} />
                         </div>
                      </div>
              <div className="row">
                 <div className="col-md-2 label-align">
<label htmlFor="txtProvince">Province:</label>
                 </div>
              <div className="col-md-5">
                 <select type="text" id="drpProvince" className="form-control"
                         onChange={this.handlerChange}
                         value={this.props.address.Province}>
   <option key="ON">Ontario</option>
   <option key="BC">BritishColumbia</option>
   <option key="QC">Quebec</option>
   <option key="ALB">Alberta</option>
   <option key="NS">NovaScotia</option>
                 </select>
              </div>
<div className="col-md-2 label-align">
<label htmlFor="txtPostalCode">Postal Code:</label>
</div>
           <div className="col-md-3">
              <input type="text" id="txtPostalCode" className="form-control"
                     onChange={this.handlerChange}
                     value={this.props.address.PostalCode} />
           </div>
              </div>
          </div>
         );
    }
});

var RestaurantReview = React.createClass({

    getInitialState: function () {
        return {
            name: this.props.data.Name,
            address: this.props.data.Location,
            summary: this.props.data.Summary,
            rating: this.props.data.Rating,
            saved: "",
            showConfirm: { display: "none" }
        };
    },

    handleAddressChange: function (newAddress) {
        this.setState({ address: newAddress });
    },
    handleSummaryChange: function (e) {
        this.setState({ summary: e.target.value });
    },
    handleRatingChange: function (e) {
        this.setState({ rating: e.target.value });
    },

    submitChange: function () {
        var restInfo = {
            name: this.state.name, Location: this.state.address,
            Summary: this.state.summary, Rating: this.state.rating
        }
        $.ajax("/Home/UpdateReview",
               {
                   
                   type: 'POST',
                   dataType: 'json',
                   data: { restInfo: JSON.stringify(restInfo) },
                   success: function (returnData) {
                       if (returnData != null) {
                           this.setState({ showConfirm: { display: "inherit" } });
                           this.setState({ saved: returnData });
                       }
                   }.bind(this)
               });
    },
    render: function () {
        return (
            <div className="row">
                <div className="col-md-9">
                <fieldset>
                    <legend>{this.state.name}</legend>

                    <Address address={this.state.address}
                             onAddressChange={this.handleAddressChange} />
           <div className="row">
               <div className="col-md-2 label-align">
                   <label htmlFor="txtSummary">Summary:</label>
               </div>
               <div className="col-md-10">
    <textarea id="txtSummary"
              className="form-control" style={{height: 100 }}
              value={this.state.summary}
              onChange={this.handleSummaryChange}></textarea>
               </div>
           </div>
           <div className="row">
               <div className="col-md-2 label-align">
                   <label htmlFor="drpRating">Rating: </label>
               </div>
<div className="col-md-10">
    <select id="drpRating" className="form-control"
            value={this.state.rating}
            onChange={this.handleRatingChange}>
    <option key="1">1</option>
    <option key="2">2</option>
    <option key="3">3</option>
    <option key="4">4</option>
    <option key="5">5</option>
    </select>
</div>
           </div>
<div className="row">
    <div className="col-md-2 label-align">
        <button className="btn btn-primary" type="button"
                onClick={this.submitChange}>
            Save
        </button>
    </div>
                        <div className="col-md-10">
                            <span className="form-control alert-success"
                                  style={this.state.showConfirm}>
                                {this.state.saved}
                            </span>
                        </div>
</div>
                </fieldset>
                </div>
            </div>
        );
    }
});
var Reviews = React.createClass({

    getInitialState: function () {
        return { data: [] };
    },

    componentDidMount: function () {
        $.ajax(this.props.initialUrl,
                    {
                       
                        type: "GET",
                        dataType: "json",
                        success: function (returnData) {
                            if (returnData != null) {
                                this.setState({ data: returnData });
                            }
                        }.bind(this),
                        error: function (jqRequest, status, e) {
                            console.log(status);
                            console.log(jqRequest);
                            console.log(e);
                        }
                    });
    },

    render: function () {

        function makeRestaurantReviewList(x) {
            return <RestaurantReview data={x} key={x.Name}
                                     updateUrl={this.props.updateUrl} />;
        }

        return (
            <div>
                <h1>Restaurant Reviews</h1>
                {this.state.data.map(makeRestaurantReviewList, this)}
            </div>
        );
    }
});

ReactDOM.render(<Reviews initialUrl="/Home/Reviews"
                         updateUrl="/Home/UpdateReview" />,
   document.getElementById("content"));
