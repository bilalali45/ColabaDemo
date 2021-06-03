export enum AssessmentData {
    militaryService = (`<p>We ask for military service to determine
    if you qualify for a Veteran Administration
    (VA) loan. VA loans are originated by
    private lenders, but are partly guaranteed by the government</p>

    <p>Visit here for more information on <a href="">VA loans.</a></p>`),

    anotherApplicant = (`<p>Many borrowers choose to apply for mortgages with co-applicants. While having a co-applicant is not a requirement, you may find it easier to qualify for a mortgage if you apply with someone else.</p>
                        <p>All applicants on the mortgage are equally responsible for payments and will end up on the property's title</p>`),

    ssn = (`<p>We use Social Security Numbers (SSN) to pull credit reports and to check credit scores. This helps us determine your creditworthiness and identify your liabilities</p>`),

    explicitConsent = (`<p>Sorry! The lawyers got to us. We'll need your explicit consent on a few issues to continue your application.</p>`),

    // criticalMorgage = (`<p>Your income is critical to determining mortgage eligibility. We’ll use income from qualified sources to estimate your ability to repay the loan</p>`),
    criticalMorgage = (`<p>Your income allows us to calculate your ability to repay your mortgage.</p><p><strong>Don’t worry about income from rental properties. We’ll ask for that later.</strong></p>`),

    assetsInfo = (`<h4>Why do we ask for asset information?</h4> 
                  <p>For two main reasons: first, this info verifies that you have sufficient funds to close on the mortgage (i.e. “cash to close”). Second, it identifies your liquid reserves in case your income goes down.</p>`),

    assetsGifts = (`<div class="accordion" id="accordionAssessment">
    <div class="card">
      <div class="card-header" id="headingOne">
        <h2 class="mb-0">
          <button onclick="document.getElementById('collapseOne').classList.toggle('show'); document.getElementById('collapseTwo').classList.remove('show')" class="btn btn-link btn-block text-left" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
          <div class="arrow"><svg xmlns="http://www.w3.org/2000/svg" width="12.281" height="7" viewBox="0 0 12.281 7">
          <path id="Path_11176" data-name="Path 11176" d="M6.141,104.14a.858.858,0,0,1-.608-.252L.253,98.609a.86.86,0,0,1,1.216-1.216l4.672,4.672,4.672-4.672a.86.86,0,0,1,1.216,1.216l-5.281,5.28A.857.857,0,0,1,6.141,104.14Z" transform="translate(-0.001 -97.14)" fill="#4e4e4e"/>
        </svg>
        </div>  
          <span class="text">What’s a “Cash Gift”?</span>
          </button>
        </h2>
      </div>  
      <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordionAssessment">
        <div class="card-body">
          Grants are forms of down payment assistance from a government agency or private organization that homeowners never have to repay – essentially gifts of free money. The State of Texas, for example, has the Homes for Texas Heroes Home Loan Program which helps teachers, police officers, firefighters, and veterans purchase homes.
        </div>
      </div>
    </div>

    <div class="card">
      <div class="card-header" id="headingTwo">
        <h2 class="mb-0">
          <button onclick="document.getElementById('collapseTwo').classList.toggle('show'); document.getElementById('collapseOne').classList.remove('show');" class="btn btn-link btn-block text-left collapsed" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
          <div class="arrow"><svg xmlns="http://www.w3.org/2000/svg" width="12.281" height="7" viewBox="0 0 12.281 7">
          <path id="Path_11176" data-name="Path 11176" d="M6.141,104.14a.858.858,0,0,1-.608-.252L.253,98.609a.86.86,0,0,1,1.216-1.216l4.672,4.672,4.672-4.672a.86.86,0,0,1,1.216,1.216l-5.281,5.28A.857.857,0,0,1,6.141,104.14Z" transform="translate(-0.001 -97.14)" fill="#4e4e4e"/>
        </svg>
        </div>  
          <span class="text">What’s a “Grant”?</span>
          </button>
        </h2>
      </div>
      <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionAssessment">
        <div class="card-body">
        A Gift of Equity occurs when a property is sold for a lower price than the current market value. This is common when family members or partners exchange property. Gifts of Equity can count towards your down payment, but require documentation.
        </div>
      </div>
    </div>
  </div>`)

}