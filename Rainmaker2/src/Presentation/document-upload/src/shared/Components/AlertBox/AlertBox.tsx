import React from 'react'

export const AlertBox = () => {
    return (
        <div className="alert-box" id="AlertBox" data-component="AlertBox">
            {/* <div className="backdrop"></div> */}
            <div className="alert-box--modal">
                <button className="alert-box--modal-close"><em className="zmdi zmdi-close"></em></button>
                <header className="alert-box--modal-header">
                    <h1 className="text-center">Helo world</h1>
                </header>
                <section className="alert-box--modal-body">
                    <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Quis provident iure quam delectus vero! Dolore quae, fuga nobis, a hic voluptas reprehenderit quis deserunt quia voluptatem expedita error at aspernatur.</p>
                </section>
                <footer className="alert-box--modal-footer">
                    <p className="text-center"><button className="btn btn-primary">OK</button></p>
                </footer>
            </div>            
        </div>
    )
}
