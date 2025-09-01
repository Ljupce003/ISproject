using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISproject.Domain.Models;
using ISproject.Repository.Data;
using ISproject.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ISproject.web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        // GET: Countries
        public IActionResult Index()
        {
            if (countryService.GetAll().Count() == 0)
            {
                FillData();
            }
            return View(countryService.GetAll());
        }

        // GET: Countries/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = countryService.GetById(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Code,Id")] Country country)
        {
            if (ModelState.IsValid)
            {
                countryService.Add(country);
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = countryService.GetById(id.Value);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Code,Id")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                countryService.Update(country);
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = countryService.GetById(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            countryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public IActionResult FillMissingData()
        {
            List<Country> countryData = GetList();

            countryService.AddAll(countryData);

            return RedirectToAction(nameof(Index));
        }

        private void FillData()
        {
            //List<Country> countryData = [
            //    new Country { Name = "Argentina", Code = "ar" },
            //    new Country { Name = "Australia", Code = "au" },
            //    new Country { Name = "Austria", Code = "at" },
            //    new Country { Name = "Belgium", Code = "be" },
            //    new Country { Name = "Brazil", Code = "br" },
            //    new Country { Name = "Bulgaria", Code = "bg" },
            //    new Country { Name = "Canada", Code = "ca" },
            //    new Country { Name = "China", Code = "cn" },
            //    new Country { Name = "Colombia", Code = "co" },
            //    new Country { Name = "Czech Republic", Code = "cz" },
            //    new Country { Name = "Egypt", Code = "eg" },
            //    new Country { Name = "France", Code = "fr" },
            //    new Country { Name = "Germany", Code = "de" },
            //    new Country { Name = "Greece", Code = "gr" },
            //    new Country { Name = "Hong Kong", Code = "hk" },
            //    new Country { Name = "Hungary", Code = "hu" },
            //    new Country { Name = "India", Code = "in" },
            //    new Country { Name = "Indonesia", Code = "id" },
            //    new Country { Name = "Ireland", Code = "ie" },
            //    new Country { Name = "Israel", Code = "il" },
            //    new Country { Name = "Italy", Code = "it" },
            //    new Country { Name = "Japan", Code = "jp" },
            //    new Country { Name = "Latvia", Code = "lv" },
            //    new Country { Name = "Lithuania", Code = "lt" },
            //    new Country { Name = "Malaysia", Code = "my" },
            //    new Country { Name = "Mexico", Code = "mx" },
            //    new Country { Name = "Morocco", Code = "ma" },
            //    new Country { Name = "Netherlands", Code = "nl" },
            //    new Country { Name = "New Zealand", Code = "nz" },
            //    new Country { Name = "Nigeria", Code = "ng" },
            //    new Country { Name = "Norway", Code = "no" },
            //    new Country { Name = "Philippines", Code = "ph" },
            //    new Country { Name = "Poland", Code = "pl" },
            //    new Country { Name = "Portugal", Code = "pt" },
            //    new Country { Name = "Romania", Code = "ro" },
            //    new Country { Name = "Saudi Arabia", Code = "sa" },
            //    new Country { Name = "Serbia", Code = "rs" },
            //    new Country { Name = "Spain", Code = "es" },
            //    new Country { Name = "Senegal", Code = "sn" },
            //    new Country { Name = "Singapore", Code = "sg" },
            //    new Country { Name = "Slovakia", Code = "sk" },
            //    new Country { Name = "Slovenia", Code = "si" },
            //    new Country { Name = "South Africa", Code = "za" },
            //    new Country { Name = "South Korea", Code = "kr" },
            //    new Country { Name = "Sweden", Code = "se" },
            //    new Country { Name = "Switzerland", Code = "ch" },
            //    new Country { Name = "Taiwan", Code = "tw" },
            //    new Country { Name = "Thailand", Code = "th" },
            //    new Country { Name = "Turkey", Code = "tr" },
            //    new Country { Name = "UAE", Code = "ae" },
            //    new Country { Name = "Ukraine", Code = "ua" },
            //    new Country { Name = "United Kingdom", Code = "gb" },
            //    new Country { Name = "United States", Code = "us" },
            //    new Country { Name = "Venezuela", Code = "ve" }
            //];




            List<Country> countryData = GetList();


            countryData.ForEach(c => { countryService.Add(c); });
        }

        private static List<Country> GetList()
        {
            return [
                new Country { Name = "Afghanistan", Code = "af" },
                new Country { Name = "Albania", Code = "al" },
                new Country { Name = "Algeria", Code = "dz" },
                new Country { Name = "Andorra", Code = "ad" },
                new Country { Name = "Angola", Code = "ao" },
                new Country { Name = "Antigua and Barbuda", Code = "ag" },
                new Country { Name = "Argentina", Code = "ar" },
                new Country { Name = "Armenia", Code = "am" },
                new Country { Name = "Australia", Code = "au" },
                new Country { Name = "Austria", Code = "at" },
                new Country { Name = "Azerbaijan", Code = "az" },
                new Country { Name = "Bahamas", Code = "bs" },
                new Country { Name = "Bahrain", Code = "bh" },
                new Country { Name = "Bangladesh", Code = "bd" },
                new Country { Name = "Barbados", Code = "bb" },
                new Country { Name = "Belarus", Code = "by" },
                new Country { Name = "Belgium", Code = "be" },
                new Country { Name = "Belize", Code = "bz" },
                new Country { Name = "Benin", Code = "bj" },
                new Country { Name = "Bhutan", Code = "bt" },
                new Country { Name = "Bolivia", Code = "bo" },
                new Country { Name = "Bosnia and Herzegovina", Code = "ba" },
                new Country { Name = "Botswana", Code = "bw" },
                new Country { Name = "Brazil", Code = "br" },
                new Country { Name = "Brunei", Code = "bn" },
                new Country { Name = "Bulgaria", Code = "bg" },
                new Country { Name = "Burkina Faso", Code = "bf" },
                new Country { Name = "Burundi", Code = "bi" },
                new Country { Name = "Cabo Verde", Code = "cv" },
                new Country { Name = "Cambodia", Code = "kh" },
                new Country { Name = "Cameroon", Code = "cm" },
                new Country { Name = "Canada", Code = "ca" },
                new Country { Name = "Central African Republic", Code = "cf" },
                new Country { Name = "Chad", Code = "td" },
                new Country { Name = "Chile", Code = "cl" },
                new Country { Name = "China", Code = "cn" },
                new Country { Name = "Colombia", Code = "co" },
                new Country { Name = "Comoros", Code = "km" },
                new Country { Name = "Congo (Congo-Brazzaville)", Code = "cg" },
                new Country { Name = "Congo (Congo-Kinshasa)", Code = "cd" },
                new Country { Name = "Costa Rica", Code = "cr" },
                new Country { Name = "Croatia", Code = "hr" },
                new Country { Name = "Cuba", Code = "cu" },
                new Country { Name = "Cyprus", Code = "cy" },
                new Country { Name = "Czech Republic", Code = "cz" },
                new Country { Name = "Denmark", Code = "dk" },
                new Country { Name = "Djibouti", Code = "dj" },
                new Country { Name = "Dominica", Code = "dm" },
                new Country { Name = "Dominican Republic", Code = "do" },
                new Country { Name = "Ecuador", Code = "ec" },
                new Country { Name = "Egypt", Code = "eg" },
                new Country { Name = "El Salvador", Code = "sv" },
                new Country { Name = "Equatorial Guinea", Code = "gq" },
                new Country { Name = "Eritrea", Code = "er" },
                new Country { Name = "Estonia", Code = "ee" },
                new Country { Name = "Eswatini", Code = "sz" },
                new Country { Name = "Ethiopia", Code = "et" },
                new Country { Name = "Fiji", Code = "fj" },
                new Country { Name = "Finland", Code = "fi" },
                new Country { Name = "France", Code = "fr" },
                new Country { Name = "Gabon", Code = "ga" },
                new Country { Name = "Gambia", Code = "gm" },
                new Country { Name = "Georgia", Code = "ge" },
                new Country { Name = "Germany", Code = "de" },
                new Country { Name = "Ghana", Code = "gh" },
                new Country { Name = "Greece", Code = "gr" },
                new Country { Name = "Grenada", Code = "gd" },
                new Country { Name = "Guatemala", Code = "gt" },
                new Country { Name = "Guinea", Code = "gn" },
                new Country { Name = "Guinea-Bissau", Code = "gw" },
                new Country { Name = "Guyana", Code = "gy" },
                new Country { Name = "Haiti", Code = "ht" },
                new Country { Name = "Honduras", Code = "hn" },
                new Country { Name = "Hungary", Code = "hu" },
                new Country { Name = "Iceland", Code = "is" },
                new Country { Name = "India", Code = "in" },
                new Country { Name = "Indonesia", Code = "id" },
                new Country { Name = "Iran", Code = "ir" },
                new Country { Name = "Iraq", Code = "iq" },
                new Country { Name = "Ireland", Code = "ie" },
                new Country { Name = "Israel", Code = "il" },
                new Country { Name = "Italy", Code = "it" },
                new Country { Name = "Jamaica", Code = "jm" },
                new Country { Name = "Japan", Code = "jp" },
                new Country { Name = "Jordan", Code = "jo" },
                new Country { Name = "Kazakhstan", Code = "kz" },
                new Country { Name = "Kenya", Code = "ke" },
                new Country { Name = "Kiribati", Code = "ki" },
                new Country { Name = "Kuwait", Code = "kw" },
                new Country { Name = "Kyrgyzstan", Code = "kg" },
                new Country { Name = "Laos", Code = "la" },
                new Country { Name = "Latvia", Code = "lv" },
                new Country { Name = "Lebanon", Code = "lb" },
                new Country { Name = "Lesotho", Code = "ls" },
                new Country { Name = "Liberia", Code = "lr" },
                new Country { Name = "Libya", Code = "ly" },
                new Country { Name = "Liechtenstein", Code = "li" },
                new Country { Name = "Lithuania", Code = "lt" },
                new Country { Name = "Luxembourg", Code = "lu" },
                new Country { Name = "Madagascar", Code = "mg" },
                new Country { Name = "Malawi", Code = "mw" },
                new Country { Name = "Malaysia", Code = "my" },
                new Country { Name = "Maldives", Code = "mv" },
                new Country { Name = "Mali", Code = "ml" },
                new Country { Name = "Malta", Code = "mt" },
                new Country { Name = "Marshall Islands", Code = "mh" },
                new Country { Name = "Mauritania", Code = "mr" },
                new Country { Name = "Mauritius", Code = "mu" },
                new Country { Name = "Mexico", Code = "mx" },
                new Country { Name = "Micronesia", Code = "fm" },
                new Country { Name = "Moldova", Code = "md" },
                new Country { Name = "Monaco", Code = "mc" },
                new Country { Name = "Mongolia", Code = "mn" },
                new Country { Name = "Montenegro", Code = "me" },
                new Country { Name = "Morocco", Code = "ma" },
                new Country { Name = "Mozambique", Code = "mz" },
                new Country { Name = "Myanmar", Code = "mm" },
                new Country { Name = "Namibia", Code = "na" },
                new Country { Name = "Nauru", Code = "nr" },
                new Country { Name = "Nepal", Code = "np" },
                new Country { Name = "Netherlands", Code = "nl" },
                new Country { Name = "New Zealand", Code = "nz" },
                new Country { Name = "Nicaragua", Code = "ni" },
                new Country { Name = "Niger", Code = "ne" },
                new Country { Name = "Nigeria", Code = "ng" },
                new Country { Name = "North Korea", Code = "kp" },
                new Country { Name = "North Macedonia", Code = "mk" },
                new Country { Name = "Norway", Code = "no" },
                new Country { Name = "Oman", Code = "om" },
                new Country { Name = "Pakistan", Code = "pk" },
                new Country { Name = "Palau", Code = "pw" },
                new Country { Name = "Palestine", Code = "ps" },
                new Country { Name = "Panama", Code = "pa" },
                new Country { Name = "Papua New Guinea", Code = "pg" },
                new Country { Name = "Paraguay", Code = "py" },
                new Country { Name = "Peru", Code = "pe" },
                new Country { Name = "Philippines", Code = "ph" },
                new Country { Name = "Poland", Code = "pl" },
                new Country { Name = "Portugal", Code = "pt" },
                new Country { Name = "Qatar", Code = "qa" },
                new Country { Name = "Romania", Code = "ro" },
                new Country { Name = "Russia", Code = "ru" },
                new Country { Name = "Rwanda", Code = "rw" },
                new Country { Name = "Saint Kitts and Nevis", Code = "kn" },
                new Country { Name = "Saint Lucia", Code = "lc" },
                new Country { Name = "Saint Vincent and the Grenadines", Code = "vc" },
                new Country { Name = "Samoa", Code = "ws" },
                new Country { Name = "San Marino", Code = "sm" },
                new Country { Name = "Sao Tome and Principe", Code = "st" },
                new Country { Name = "Saudi Arabia", Code = "sa" },
                new Country { Name = "Senegal", Code = "sn" },
                new Country { Name = "Serbia", Code = "rs" },
                new Country { Name = "Seychelles", Code = "sc" },
                new Country { Name = "Sierra Leone", Code = "sl" },
                new Country { Name = "Singapore", Code = "sg" },
                new Country { Name = "Slovakia", Code = "sk" },
                new Country { Name = "Slovenia", Code = "si" },
                new Country { Name = "Solomon Islands", Code = "sb" },
                new Country { Name = "Somalia", Code = "so" },
                new Country { Name = "South Africa", Code = "za" },
                new Country { Name = "South Korea", Code = "kr" },
                new Country { Name = "South Sudan", Code = "ss" },
                new Country { Name = "Spain", Code = "es" },
                new Country { Name = "Sri Lanka", Code = "lk" },
                new Country { Name = "Sudan", Code = "sd" },
                new Country { Name = "Suriname", Code = "sr" },
                new Country { Name = "Sweden", Code = "se" },
                new Country { Name = "Switzerland", Code = "ch" },
                new Country { Name = "Syria", Code = "sy" },
                new Country { Name = "Taiwan", Code = "tw" },
                new Country { Name = "Tajikistan", Code = "tj" },
                new Country { Name = "Tanzania", Code = "tz" },
                new Country { Name = "Thailand", Code = "th" },
                new Country { Name = "Timor-Leste", Code = "tl" },
                new Country { Name = "Togo", Code = "tg" },
                new Country { Name = "Tonga", Code = "to" },
                new Country { Name = "Trinidad and Tobago", Code = "tt" },
                new Country { Name = "Tunisia", Code = "tn" },
                new Country { Name = "Turkey", Code = "tr" },
                new Country { Name = "Turkmenistan", Code = "tm" },
                new Country { Name = "Tuvalu", Code = "tv" },
                new Country { Name = "Uganda", Code = "ug" },
                new Country { Name = "Ukraine", Code = "ua" },
                new Country { Name = "United Arab Emirates", Code = "ae" },
                new Country { Name = "United Kingdom", Code = "gb" },
                new Country { Name = "United Kingdom", Code = "uk" },
                new Country { Name = "United States", Code = "us" },
                new Country { Name = "Uruguay", Code = "uy" },
                new Country { Name = "Uzbekistan", Code = "uz" },
                new Country { Name = "Vanuatu", Code = "vu" },
                new Country { Name = "Vatican City", Code = "va" },
                new Country { Name = "Venezuela", Code = "ve" },
                new Country { Name = "Vietnam", Code = "vn" },
                new Country { Name = "Yemen", Code = "ye" },
                new Country { Name = "Zambia", Code = "zm" },
                new Country { Name = "Zimbabwe", Code = "zw" }
            ];
        }

    }
    }
