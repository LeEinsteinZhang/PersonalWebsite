from flask import Flask, render_template, request, redirect, url_for, session
import json
import os

app = Flask(__name__)
app.secret_key = '233'
app.url_map.strict_slashes = False

def load_translation(page, lang):
    with open(os.path.join('translations', page, f'{lang}.json'), 'r', encoding='utf-8') as file:
        return json.load(file)

@app.route('/')
def home():
    if 'lang' in session:
        lang = session['lang']
    else:
        lang = request.accept_languages.best_match(['zh', 'en'])
        if lang is None or lang not in ['zh', 'en']:
            lang = 'en'
        session['lang'] = lang
    return redirect(url_for('home_lang', lang=lang))

@app.route('/<lang>')
def home_lang(lang):
    if 'lang' not in session or session['lang'] != lang:
        session['lang'] = lang
    translations = load_translation('index', lang)
    return render_template('index.html', translations=translations, lang=lang)

@app.route('/<lang>/career')
def career_lang(lang):
    if 'lang' not in session or session['lang'] != lang:
        session['lang'] = lang
    translations = load_translation('career', lang)
    return render_template('career.html', translations=translations, lang=lang)

@app.route('/<lang>/career/modeling_engineer')
def job_modeling_engineer(lang):
    if 'lang' not in session or session['lang'] != lang:
        session['lang'] = lang
    translations = load_translation('job/modeling_engineer', lang)
    return render_template('job.html', translations=translations, lang=lang)

@app.route('/<lang>/career/scripts_developer')
def job_scripts_developer(lang):
    if 'lang' not in session or session['lang'] != lang:
        session['lang'] = lang
    translations = load_translation('job/scripts_developer', lang)
    return render_template('job.html', translations=translations, lang=lang)

@app.route('/<lang>/career/web_developer')
def job_web_developer(lang):
    if 'lang' not in session or session['lang'] != lang:
        session['lang'] = lang
    translations = load_translation('job/web_developer', lang)
    return render_template('job.html', translations=translations, lang=lang)

if __name__ == '__main__':
    app.run()
